namespace Review.Application.Interfaces.Commands;

public interface ICreateCommandHandler<in TCommand, TResponseDto> : ICommandHandler<TCommand, TResponseDto>
    where TCommand : ICreateCommand<TResponseDto>;