using MediatR;
using Shared.Wrappers;

namespace Book.Application.Interfaces.Commands;

public interface IUpdateCommandHandler<in TCommand, TResponseDto> : IRequestHandler<TCommand, Response<TResponseDto>>
    where TCommand : IUpdateCommand<TResponseDto>;