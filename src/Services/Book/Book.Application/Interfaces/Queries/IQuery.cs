using MediatR;
using Shared.Wrappers;

namespace Book.Application.Interfaces.Queries;

public interface IQuery<TResponseDto> : IRequest<Response<IEnumerable<TResponseDto>>>;