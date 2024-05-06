using Book.Application.DTOs.Language.RequestDTOs;
using Book.Application.Interfaces.Commands;

namespace Book.Application.Features.LanguageFeatures.Commands.AddLanguageToBook;

public sealed record AddLanguageToBookCommand(AddLanguageToBookDto Dto) : ICommand;