namespace Identify.Domain.Shared;

public class ValidationFailedResponse<T>(IEnumerable<Error> errors)
    : Response<T>(default, false, IValidationFailedResponse.ValidationError), IValidationFailedResponse
{
    public IEnumerable<Error> Errors { get; } = errors;

    public static ValidationFailedResponse<T> WithErrors(IEnumerable<Error> errors) => new(errors);
}