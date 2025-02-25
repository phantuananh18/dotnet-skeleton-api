using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using System.Net;

namespace DotnetSkeleton.API.Extensions.Middleware.ExceptionHandling
{
    public sealed class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = true
            };
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the error handler will not be executed.");
                }
                else
                {
                    _logger.LogError(ex, $"An unexpected exception: {ex.Source} - {ex.Message}");
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";

                    var response = new BaseResponse()
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Code = HttpStatusCode.InternalServerError.ToString(),
                        Message = "An unexpected server error occurred, please try again.",
                        Data = null,
                        Errors = null
                    };

                    var json = JsonSerializer.Serialize(response, _jsonSerializerOptions);
                    await context.Response.WriteAsync(json);
                }
            }
        }
    }
}
