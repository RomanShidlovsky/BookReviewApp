using AutoMapper;
using Book.Application.DTOs.Book.ResponseDTOs;
using Book.Application.DTOs.EventBus;
using Book.Application.Interfaces.Commands;
using Book.Domain.Errors;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Repositories;
using MassTransit;
using RabbitMQ.EventBus.Interfaces.BookMessages;
using BookEntity = Book.Domain.Entities.Book;
using Response = Shared.Wrappers.Response;

namespace Book.Application.Features.BookFeatures.Commands.Create;

public class CreateBookCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper, IPublishEndpoint _publishEndpoint)
    : ICreateCommandHandler<CreateBookCommand, BookResponseDto>
{
    public async Task<Shared.Wrappers.Response<BookResponseDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<IBookRepository>();
        var dto = request.Dto;

        if (dto.OpenLibraryKey is not null)
        {
            var existingBook = await repository.GetByOpenLibraryKeyAsync(dto.OpenLibraryKey, cancellationToken);

            if (existingBook is not null)
            {
                return Response.Failure<BookResponseDto>(DomainErrors.Book.OpenLibraryKeyConflict);
            }
        }
        
        var book = _mapper.Map<BookEntity>(dto);

        repository.Create(book);
        await _unitOfWork.SaveAsync(cancellationToken);

        await _publishEndpoint.Publish<IBookCreated>(new BookCreated(book.Id, book.Title, book.ImageUrl),
            cancellationToken);

        await Console.Out.WriteLineAsync($"BookCreated with Id = {book.Id} published.");

        return _mapper.Map<BookResponseDto>(book);
    }
}