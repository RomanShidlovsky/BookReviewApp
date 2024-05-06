using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Shared.Wrappers;

public static class ApiResponse
{
    public static ObjectResult GetObjectResult<T>(Response<T> response, ILogger? logger = null)
    {
        if (response.Succeeded)
        {
            logger?.LogInformation("Successful response: {@Response}", response);
            return new OkObjectResult(response.Value);
        }

        if (response is IValidationFailedResponse validationError)
        {
            logger?.LogError("Validation failed: {@Errors}", validationError.Errors);
            return new ObjectResult(validationError.Errors)
            {
                StatusCode = response.Error.ErrorStatusCode,
                ContentTypes = { "application/json" }
            };
        }

        logger?.LogError("Error response: {@Error}", response.Error);
        return new ObjectResult(response.Error)
        {
            StatusCode = response.Error.ErrorStatusCode,
            ContentTypes = { "application/json" }
        };
    }

    public static ObjectResult GetObjectResult(Response response, ILogger? logger = null)
    {
        if (response.Succeeded)
        {
            logger?.LogInformation("Successful response");
            return new OkObjectResult(response.Succeeded);
        }

        if (response is IValidationFailedResponse validationError)
        {
            logger?.LogError("Validation failed: {@Errors}", validationError.Errors);
            return new ObjectResult(validationError.Errors)
            {
                StatusCode = response.Error.ErrorStatusCode,
                ContentTypes = { "application/json" }
            };
        }
        else
        {
            logger?.LogError("Error response: {@Error}", response.Error);
            return new ObjectResult(response.Error)
            {
                StatusCode = response.Error.ErrorStatusCode,
                ContentTypes = { "application/json" }
            };
        }
    }
}