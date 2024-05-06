using Book.Application.DTOs.Language.ResponseDTOs;
using Book.Application.Interfaces.Queries;

namespace Book.Application.Features.LanguageFeatures.Queries.GetByName;

public sealed record GetLanguageByNameQuery(string Name) : ISingleQuery<LanguageResponseDto>;