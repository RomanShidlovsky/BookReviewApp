using MediatR;
using Shared.Wrappers;

namespace Book.Application.Interfaces.Commands;

public interface IUpdateCommand<TResponseDto> : ICommand<TResponseDto>;