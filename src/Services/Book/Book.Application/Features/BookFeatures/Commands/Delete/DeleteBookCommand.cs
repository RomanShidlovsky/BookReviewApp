using Book.Application.Interfaces.Commands;

namespace Book.Application.Features.BookFeatures.Commands.Delete;

public sealed record DeleteBookCommand(int Id) : IDeleteCommand;