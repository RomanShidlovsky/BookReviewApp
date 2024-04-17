using MediatR;
using Shared.Wrappers;

namespace Review.Application.Interfaces.Queries;

public interface ISingleQuery<TResponseDto> : IRequest<Response<TResponseDto>>; 
