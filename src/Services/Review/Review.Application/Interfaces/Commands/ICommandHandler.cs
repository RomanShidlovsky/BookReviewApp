using MediatR;
using Shared.Wrappers;

namespace Review.Application.Interfaces.Commands;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Response>
    where TCommand : ICommand;
    
public interface ICommandHandler<in TCommand, TResponseDto> : IRequestHandler<TCommand, Response<TResponseDto>>
    where TCommand : ICommand<TResponseDto>;