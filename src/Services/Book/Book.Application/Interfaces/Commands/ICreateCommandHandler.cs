using MediatR;
using Shared.Wrappers;

namespace Book.Application.Interfaces.Commands;

public interface ICreateCommandHandler<in TCommand, TResponseDto> : IRequestHandler<TCommand, Response<TResponseDto>>
    where TCommand : ICreateCommand<TResponseDto>;