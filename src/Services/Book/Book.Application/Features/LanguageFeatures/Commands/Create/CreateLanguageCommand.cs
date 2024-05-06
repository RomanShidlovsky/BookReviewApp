using Book.Application.DTOs.Language.RequestDTOs;
using Book.Application.DTOs.Language.ResponseDTOs;
using Book.Application.Interfaces.Commands;

namespace Book.Application.Features.LanguageFeatures.Commands.Create;

public sealed record CreateLanguageCommand(CreateLanguageDto Dto) : ICreateCommand<LanguageResponseDto>;