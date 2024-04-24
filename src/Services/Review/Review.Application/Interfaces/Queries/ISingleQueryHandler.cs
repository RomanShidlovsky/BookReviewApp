using MediatR;
using Shared.Wrappers;

namespace Review.Application.Interfaces.Queries;

public interface ISingleQueryHandler<in TQuery, TResponseDto> : IRequestHandler<TQuery, Response<TResponseDto>>
    where TQuery : ISingleQuery<TResponseDto>;