using AutoMapper;
using Book.Application.DTOs.Book.ResponseDTOs;
using Book.Application.Interfaces.Commands;
using Book.Domain.Errors;
using Book.Domain.Extensions;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.BookFeatures.Commands.Update;

public class UpdateBookCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
    : IUpdateCommandHandler<UpdateBookCommand, BookResponseDto>
{
    public async Task<Response<BookResponseDto>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<IBookRepository>();
        var dto = request.Dto;

        var book = await repository.GetByIdAsync(dto.Id, cancellationToken);

        if (book is null)
        {
            return Response.Failure<BookResponseDto>(DomainErrors.Book.BookNotFoundById);
        }
        
        if (dto.OpenLibraryKey is not null)
        {
            var openLibraryKeyAuthor =
                await repository.GetAsync(b => 
                        b.Id != dto.Id && b.IsOpenLibraryKey(dto.OpenLibraryKey), cancellationToken);

            if (openLibraryKeyAuthor.Count != 0)
            {
                return Response.Failure<BookResponseDto>(DomainErrors.Book.OpenLibraryKeyConflict);
            }
        }

        _mapper.Map(dto, book);

        repository.Update(book);
        await _unitOfWork.SaveAsync(cancellationToken);

        return _mapper.Map<BookResponseDto>(book);
    }
}