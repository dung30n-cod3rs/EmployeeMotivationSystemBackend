using System.Net;
using System.Text.Json;

namespace EmployeeMotivationSystem.API.Middleware.Exceptions;

public class ExceptionCatcherMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var result = new ExceptionCatcherMiddlewareModel
        {
            ErrorMessage = exception.Message
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
        
        return context.Response.WriteAsync(JsonSerializer.Serialize(result));
    }
}