using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.UserFeatures.Commands.Delete;

public sealed record DeleteUserCommand(int Id) : IDeleteCommand;