using AutoMapper;
using DotnetSkeleton.Application.HealthChecks;
using DotnetSkeleton.Core.Application.HealthChecks;
using DotnetSkeleton.Core.Application.Services;
using DotnetSkeleton.Core.Domain.Interfaces.Services;
using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;

namespace DotnetSkeleton.Core.Application;

/// <summary>
/// Class for handling dependency injection.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds application services to the service collection.
    /// </summary>
    public static void AddCoreModuleApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();

        var retryOptions = configuration.GetSection(HttpClientRetryOptions.JsonKey)
            .Get<HttpClientRetryOptions>();
        services.AddHttpClient()
            .AddResiliencePipeline("pipeline", builder =>
            {
                builder.AddRetry(new RetryStrategyOptions()
                {
                    MaxRetryAttempts = retryOptions!.MaxRetryAttempts,
                    Delay = TimeSpan.FromSeconds(retryOptions.Delay),
                    BackoffType = DelayBackoffType.Exponential
                });
                builder.AddTimeout(TimeSpan.FromSeconds(retryOptions.Timeout));
            });
    }

    /// <summary>
    /// Adds services to the service collection.
    /// </summary>
    private static void AddServices(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck<MySQLHealthCheck>(Constant.SystemInfo.MySqlDatabase)
            .AddCheck<MongoDBHealthCheck>(Constant.SystemInfo.MongoDbDatabase);
        services.AddAutoMapper();

        services.AddScoped<IHealthService, HealthService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IRolePermissionService, RolePermissionService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IMessageService, MessageService>();
    }

    private static void AddAutoMapper(this IServiceCollection services)
    {
        var profiles = new Profile[]
        {
        };

        var mapper = new MapperConfiguration(options => options.AddProfiles(profiles)).CreateMapper();

        services.AddSingleton(mapper);
    }
}