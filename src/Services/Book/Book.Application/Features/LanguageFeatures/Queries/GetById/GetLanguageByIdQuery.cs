using Book.Application.DTOs.Language.ResponseDTOs;
using Book.Application.Interfaces.Queries;

namespace Book.Application.Features.LanguageFeatures.Queries.GetById;

public sealed record GetLanguageByIdQuery(int Id) : ISingleQuery<LanguageResponseDto>;