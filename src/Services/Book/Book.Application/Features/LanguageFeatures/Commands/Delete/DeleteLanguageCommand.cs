using Book.Application.Interfaces.Commands;

namespace Book.Application.Features.LanguageFeatures.Commands.Delete;

public sealed record DeleteLanguageCommand(int Id) : IDeleteCommand;