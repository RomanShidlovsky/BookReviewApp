using AutoMapper;
using Book.Application.DTOs.Book.ResponseDTOs;
using Book.Application.Interfaces.Queries;
using Book.Domain.Interfaces.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.BookFeatures.Queries.GetAll;

public class GetAllBooksQueryHandler(IBookRepository _repository, IMapper _mapper)
    : IQueryHandler<GetAllBooksQuery, BookResponseDto>
{
    public async Task<Response<IEnumerable<BookResponseDto>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _repository.GetAllAsync(cancellationToken);

        return _mapper.Map<List<BookResponseDto>>(books);
    }
}