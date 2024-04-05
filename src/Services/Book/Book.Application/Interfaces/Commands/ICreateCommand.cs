using MediatR;
using Shared.Wrappers;

namespace Book.Application.Interfaces.Commands;

public interface ICreateCommand<TResponseDto> : ICommand<TResponseDto>;