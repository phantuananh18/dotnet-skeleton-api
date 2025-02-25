using DotnetSkeleton.API.Extensions.Middleware;
using DotnetSkeleton.API.Extensions.Middleware.ApiHeaderHandling;
using DotnetSkeleton.API.Extensions.Middleware.ExceptionHandling;
using DotnetSkeleton.API.Extensions.Middleware.LoggingHandling;
using DotnetSkeleton.API.Extensions.Middleware.Validation;

namespace DotnetSkeleton.API.Extensions
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder AddMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApiHeaderHandlingMiddleware>();
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<ValidationMiddleware>();
            app.UseMiddleware<JwtMiddleware>();
            return app;
        }
    }
}
