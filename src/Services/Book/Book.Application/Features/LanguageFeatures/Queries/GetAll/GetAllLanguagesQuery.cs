using Book.Application.DTOs.Language.ResponseDTOs;
using Book.Application.Interfaces.Queries;

namespace Book.Application.Features.LanguageFeatures.Queries.GetAll;

public sealed record GetAllLanguagesQuery : IQuery<LanguageResponseDto>;