using MediatR;
using Shared.Wrappers;

namespace Book.Application.Interfaces.Commands;

public interface ICommand : IRequest<Response>;

public interface ICommand<TResponseDto> : IRequest<Response<TResponseDto>>;