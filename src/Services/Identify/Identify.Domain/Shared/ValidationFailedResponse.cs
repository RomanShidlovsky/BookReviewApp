namespace Identify.Domain.Shared;

public class ValidationFailedResponse(IEnumerable<Error> errors) 
    : Response(false, IValidationFailedResponse.ValidationError), IValidationFailedResponse
{
    public IEnumerable<Error> Errors { get; } = errors;

    public static ValidationFailedResponse WithErrors(IEnumerable<Error> errors) => new(errors);
}