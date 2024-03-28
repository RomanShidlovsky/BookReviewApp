using Microsoft.AspNetCore.Mvc;
using Shared.Wrappers;

namespace Shared;

public static class ApiResponse
{
    public static ObjectResult GetObjectResult<T>(Response<T> response)
    {
        return response.Succeeded
            ? new ObjectResult(response.Value)
                { StatusCode = 200, ContentTypes = { "application/json" } }
            : response is IValidationFailedResponse validationError
                ? new ObjectResult(validationError.Errors)
                    { StatusCode = response.Error.ErrorStatusCode, ContentTypes = { "application/json" } }
                : new ObjectResult(response.Error)
                    { StatusCode = response.Error.ErrorStatusCode, ContentTypes = { "application/json" } };
    }

    public static ObjectResult GetObjectResult(Response response)
    {
        return response.Succeeded
            ? new ObjectResult(response.Succeeded)
                { StatusCode = 200, ContentTypes = { "application/json" } }
            : response is IValidationFailedResponse validationError
                ? new ObjectResult(validationError.Errors)
                    { StatusCode = response.Error.ErrorStatusCode, ContentTypes = { "application/json" } }
                : new ObjectResult(response.Error)
                    { StatusCode = response.Error.ErrorStatusCode, ContentTypes = { "application/json" } };
    }
}