using AutoMapper;
using Book.Application.DTOs.Book.ResponseDTOs;
using Book.Application.Interfaces.Queries;
using Book.Domain.Errors;
using Book.Domain.Interfaces.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.BookFeatures.Queries.GetById;

public class GetBookByIdQueryHandler(IBookRepository _repository, IMapper _mapper)
    : ISingleQueryHandler<GetBookByIdQuery, BookResponseDto>
{
    public async Task<Response<BookResponseDto>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _repository.GetByIdAsync(request.Id, cancellationToken);

        return book == null
            ? Response.Failure<BookResponseDto>(DomainErrors.Book.BookNotFoundById)
            : _mapper.Map<BookResponseDto>(book);
    }
}