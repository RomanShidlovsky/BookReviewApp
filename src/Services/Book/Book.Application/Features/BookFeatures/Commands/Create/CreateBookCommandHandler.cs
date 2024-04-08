using AutoMapper;
using Book.Application.DTOs.Author.ResponseDTOs;
using Book.Application.DTOs.Book.ResponseDTOs;
using Book.Application.Interfaces.Commands;
using Book.Domain.Errors;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Repositories;
using Shared.Wrappers;
using BookEntity = Book.Domain.Entities.Book;

namespace Book.Application.Features.BookFeatures.Commands.Create;

public class CreateBookCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
    : ICreateCommandHandler<CreateBookCommand, BookResponseDto>
{
    public async Task<Response<BookResponseDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<IBookRepository>();
        var dto = request.Dto;

        if (dto.OpenLibraryKey != null)
        {
            var existingBook = await repository.GetByOpenLibraryKey(dto.OpenLibraryKey, cancellationToken);
            
            if (existingBook != null)
                return Response.Failure<BookResponseDto>(DomainErrors.Book.OpenLibraryKeyConflict);
        }
        
        var book = _mapper.Map<BookEntity>(dto);

        repository.Create(book);
        await _unitOfWork.SaveAsync(cancellationToken);

        return _mapper.Map<BookResponseDto>(book);
    }
}