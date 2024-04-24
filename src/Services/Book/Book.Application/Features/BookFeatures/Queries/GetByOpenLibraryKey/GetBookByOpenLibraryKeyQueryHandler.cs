using AutoMapper;
using Book.Application.DTOs.Book.ResponseDTOs;
using Book.Application.Interfaces.Queries;
using Book.Domain.Interfaces.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.BookFeatures.Queries.GetByOpenLibraryKey;

public class GetBookByOpenLibraryKeyQueryHandler(IBookRepository _repository, IMapper _mapper)
    : ISingleQueryHandler<GetBookByOpenLibraryKeyQuery, BookResponseDto>
{
    public async Task<Response<BookResponseDto>> Handle(GetBookByOpenLibraryKeyQuery request, CancellationToken cancellationToken)
    {
        var book = await _repository.GetByOpenLibraryKeyAsync(request.OpenLibraryKey, cancellationToken);

        return _mapper.Map<BookResponseDto>(book);
    }
}