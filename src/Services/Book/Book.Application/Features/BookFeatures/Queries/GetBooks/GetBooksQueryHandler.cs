using AutoMapper;
using Book.Application.DTOs.Book.ResponseDTOs;
using Book.Application.Interfaces.Queries;
using Book.Domain.Interfaces.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.BookFeatures.Queries.GetBooks;

public class GetBooksQueryHandler(IBookRepository _repository, IMapper _mapper)
    : IQueryHandler<GetBooksQuery, BookResponseDto>
{
    public async Task<Response<IEnumerable<BookResponseDto>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _repository.GetBooksAsync(request.PageNumber, request.PageSize, 
            request.FilterQueryString, request.OrderByQueryString, cancellationToken);

        return _mapper.Map<List<BookResponseDto>>(books);
    }
}