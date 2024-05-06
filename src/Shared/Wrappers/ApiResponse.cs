using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Shared.Wrappers;

public static class ApiResponse
{
    public static ObjectResult GetObjectResult<T>(Response<T> response)
    {
        return response.Succeeded
            ? new OkObjectResult(response.Value)
            : response is IValidationFailedResponse validationError
                ? new ObjectResult(validationError.Errors)
                    { StatusCode = response.Error.ErrorStatusCode, ContentTypes = { "application/json" } }
                : new ObjectResult(response.Error)
                    { StatusCode = response.Error.ErrorStatusCode, ContentTypes = { "application/json" } };
    }

    public static ObjectResult GetObjectResult(Response response)
    {
        return response.Succeeded
            ? new OkObjectResult(response.Succeeded)
            : response is IValidationFailedResponse validationError
                ? new ObjectResult(validationError.Errors)
                    { StatusCode = response.Error.ErrorStatusCode, ContentTypes = { "application/json" } }
                : new ObjectResult(response.Error)
                    { StatusCode = response.Error.ErrorStatusCode, ContentTypes = { "application/json" } };
    }
}