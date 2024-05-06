namespace Review.Application.Interfaces.Commands;

public interface IUpdateCommandHandler<in TCommand, TResponseDto> : ICommandHandler<TCommand, TResponseDto>
    where TCommand : IUpdateCommand<TResponseDto>;