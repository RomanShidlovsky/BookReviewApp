using System.Diagnostics;
using FluentValidation;
using MediatR;
using Shared;
using Shared.Wrappers;

namespace Book.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Response
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }
        
        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(
            validators
                .Select(v => v.ValidateAsync(context, cancellationToken)));
        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .Select(f => new Error(
                f.PropertyName,
                f.ErrorMessage))
            .Distinct()
            .ToList();

        if (failures.Count != 0)
        {
            return CreateValidationFailedResponse<TResponse>(failures);
        }
        
        return await next();
    }

    private static TResult CreateValidationFailedResponse<TResult>(IEnumerable<Error> errors)
        where TResult : Response
    {
        if (typeof(TResult) == typeof(Response))
            return (ValidationFailedResponse.WithErrors(errors) as TResult)!;

        var validationResult = typeof(ValidationFailedResponse<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationFailedResponse.WithErrors))!
            .Invoke(null, [errors])!;

        return (TResult)validationResult;
    }
}