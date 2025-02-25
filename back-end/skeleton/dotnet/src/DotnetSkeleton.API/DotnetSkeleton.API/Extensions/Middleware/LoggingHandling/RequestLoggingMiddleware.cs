using System.Diagnostics;

namespace DotnetSkeleton.API.Extensions.Middleware.LoggingHandling;

public sealed class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var traceId = Activity.Current?.TraceId.ToString() ?? context.TraceIdentifier;
        context.Items.Add("TraceId", traceId);

        _logger.LogInformation($"[DC8-Framework] Start request {traceId} with path: {context.Request.Path}, method: {context.Request.Method}");

        await _next(context);

        _logger.LogInformation($"[DC8-Framework] End request {traceId} with status code: {context.Response.StatusCode}");
    }
}