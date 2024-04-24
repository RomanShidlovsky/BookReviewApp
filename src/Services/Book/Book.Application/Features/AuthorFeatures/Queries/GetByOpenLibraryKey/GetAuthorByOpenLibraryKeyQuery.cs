using Book.Application.DTOs.Author.ResponseDTOs;
using Book.Application.Interfaces.Queries;

namespace Book.Application.Features.AuthorFeatures.Queries.GetByOpenLibraryKey;

public sealed record GetAuthorByOpenLibraryKeyQuery(string OpenLibraryKey) : ISingleQuery<AuthorResponseDto>;