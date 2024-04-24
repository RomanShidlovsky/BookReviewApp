using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Shared.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode status = HttpStatusCode.InternalServerError;
        string message = exception.Message;
        string? stackTrace = exception.StackTrace;

        var exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        logger.LogError(exception, message);
        
        return context.Response.WriteAsync(exceptionResult);
    }
}