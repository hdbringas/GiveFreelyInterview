using System.Net;
using System.Text.Json;

namespace AffiliateProgramManagementSystem.Exceptions;

/// <summary>
/// Middleware that will intercept uncaught exceptions in the HTTP pipeline and format a valid and useful response message in JSON format
/// while also logging the error to traceability.
/// </summary>
public class ApiExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ApiExceptionHandler> _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    public ApiExceptionHandler(RequestDelegate next, ILogger<ApiExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Invoke
    /// </summary>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    /// <summary>
    /// HandleExceptionAsync
    /// </summary>
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var apiException = exception as ApiException;
        dynamic? returnData = null;

        if (apiException != null)
        {
            context.Response.StatusCode = apiException.GetHttpStatusCode();
            returnData = apiException.ReturnData;
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        ApiError error = new ApiError
        {
            TraceId = System.Diagnostics.Activity.Current?.TraceId.ToHexString(),
            Type = exception.GetType().Name,
            Message = exception.Message,
            Data = returnData
        };

        _logger.LogError(exception, "HandleExceptionAsync: '{TraceId}'", error.TraceId);

        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize<object>(error));
    }

}
