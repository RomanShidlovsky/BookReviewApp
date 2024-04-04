using MediatR;
using Shared.Wrappers;

namespace Book.Application.Interfaces.Commands;

public interface IDeleteCommandHandler<in TCommand> : IRequestHandler<TCommand, Response>
    where TCommand : IDeleteCommand;