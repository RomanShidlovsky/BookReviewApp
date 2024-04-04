using Book.Domain.Errors;
using MediatR;
using Shared;
using Shared.Wrappers;

namespace Book.Application.Behaviors;

public class UnknownErrorCatchingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Response
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            return (TResponse)Response.Failure(new Error("Unknown.UnknownError", ex.Message));
        }
    }
}