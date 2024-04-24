using Book.Application.DTOs.Language.RequestDTOs;
using Book.Application.Interfaces.Commands;

namespace Book.Application.Features.LanguageFeatures.Commands.RemoveLanguageFromBook;

public sealed record RemoveLanguageFromBookCommand(RemoveLanguageFromBookDto Dto) : ICommand; 