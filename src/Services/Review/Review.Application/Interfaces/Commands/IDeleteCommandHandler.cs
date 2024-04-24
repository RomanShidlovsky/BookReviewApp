namespace Review.Application.Interfaces.Commands;

public interface IDeleteCommandHandler<in TCommand> : ICommandHandler<TCommand>
    where TCommand : IDeleteCommand;