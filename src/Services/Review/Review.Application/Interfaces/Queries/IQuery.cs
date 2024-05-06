using MediatR;
using Shared.Wrappers;

namespace Review.Application.Interfaces.Queries;

public interface IQuery<TResponseDto> : IRequest<Response<IEnumerable<TResponseDto>>>;