using Book.Application.DTOs.Author.ResponseDTOs;
using Book.Application.Interfaces.Queries;

namespace Book.Application.Features.AuthorFeatures.Queries.GetById;

public sealed record GetAuthorByIdQuery(int Id) : ISingleQuery<AuthorResponseDto>;