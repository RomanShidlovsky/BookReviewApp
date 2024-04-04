using MediatR;
using Shared.Wrappers;

namespace Book.Application.Interfaces.Queries;

public interface ISingleQuery<TResponseDto> : IRequest<Response<TResponseDto>>; 
