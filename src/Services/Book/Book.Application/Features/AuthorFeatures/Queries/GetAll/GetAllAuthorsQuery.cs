using Book.Application.DTOs.Author.ResponseDTOs;
using Book.Application.Interfaces.Queries;

namespace Book.Application.Features.AuthorFeatures.Queries.GetAll;

public sealed record GetAllAuthorsQuery : IQuery<AuthorResponseDto>;