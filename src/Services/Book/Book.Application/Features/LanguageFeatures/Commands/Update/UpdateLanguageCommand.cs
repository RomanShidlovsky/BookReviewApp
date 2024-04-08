using Book.Application.DTOs.Author.RequestDTOs;
using Book.Application.DTOs.Language.RequestDTOs;
using Book.Application.DTOs.Language.ResponseDTOs;
using Book.Application.Interfaces.Commands;

namespace Book.Application.Features.LanguageFeatures.Commands.Update;

public sealed record UpdateLanguageCommand(UpdateLanguageDto Dto) : IUpdateCommand<LanguageResponseDto>;