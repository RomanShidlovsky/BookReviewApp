using AutoMapper;
using Book.Application.DTOs.Book.ResponseDTOs;
using Book.Application.Interfaces.Queries;
using Book.Domain.Interfaces.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.BookFeatures.Queries.GetRecommendedBooks;

public class GetRecommendedBooksQueryHandler(IBookRepository _repository, IMapper _mapper)
    : IQueryHandler<GetRecommendedBooksQuery, BookResponseDto>
{
    public async Task<Response<IEnumerable<BookResponseDto>>> Handle(GetRecommendedBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _repository.GetBooksBySubjectMatchesAsync(request.BookId, request.Count, cancellationToken);

        return _mapper.Map<List<BookResponseDto>>(books);
    }
}