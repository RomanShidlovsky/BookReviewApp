using AutoMapper;
using Book.Application.DTOs.Author.ResponseDTOs;
using Book.Application.Interfaces.Queries;
using Book.Domain.Errors;
using Book.Domain.Interfaces.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.AuthorFeatures.Queries.GetByOpenLibraryKey;

public class GetAuthorByOpenLibraryKeyQueryHandler(IAuthorRepository _repository, IMapper _mapper)
    : ISingleQueryHandler<GetAuthorByOpenLibraryKeyQuery, AuthorResponseDto>
{
    public async Task<Response<AuthorResponseDto>> Handle(GetAuthorByOpenLibraryKeyQuery request, CancellationToken cancellationToken)
    {
        var author = await _repository.GetByOpenLibraryKeyAsync(request.OpenLibraryKey, cancellationToken);

        return author is null
            ? Response.Failure<AuthorResponseDto>(DomainErrors.Author.AuthorNotFoundByOpenLibraryKey)
            : _mapper.Map<AuthorResponseDto>(author);
    }
}