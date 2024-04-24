using Book.Application.Interfaces.Commands;

namespace Book.Application.Features.AuthorFeatures.Commands.Delete;

public sealed record DeleteAuthorCommand(int Id) : IDeleteCommand;