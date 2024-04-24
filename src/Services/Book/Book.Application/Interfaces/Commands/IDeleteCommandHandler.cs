using MediatR;
using Shared.Wrappers;

namespace Book.Application.Interfaces.Commands;

public interface IDeleteCommandHandler<in TCommand> : ICommandHandler<TCommand>
    where TCommand : IDeleteCommand;