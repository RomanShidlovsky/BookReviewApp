using MediatR;
using Shared.Wrappers;

namespace Book.Application.Interfaces.Queries;

public interface IQueryHandler<in TQuery, TResponseDto> : IRequestHandler<TQuery, Response<IEnumerable<TResponseDto>>>
    where TQuery : IQuery<TResponseDto>; 