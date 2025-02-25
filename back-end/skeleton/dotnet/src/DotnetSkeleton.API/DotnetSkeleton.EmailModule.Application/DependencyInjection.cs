using DotnetSkeleton.EmailModule.Application.HealthChecks;
using DotnetSkeleton.EmailModule.Application.Mappings;
using DotnetSkeleton.EmailModule.Application.Services;
using DotnetSkeleton.EmailModule.Domain.Interfaces.Services;
using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DotnetSkeleton.EmailModule.Application;

public static class DependencyInjection
{
    /// <summary>
    /// Adds application services to the service collection.
    /// </summary>
    public static void AddEmailModuleApplication(this IServiceCollection services)
    {
        services.AddServices();
        services.AddAutoMapper();
    }

    /// <summary>
    /// Adds services to the service collection.
    /// </summary>
    private static void AddServices(this IServiceCollection services)
    {
        // Add Health Checks Service
        services.AddHealthChecks().AddCheck<EmailModuleHealthCheck>(Constant.SystemInfo.EmailModule);

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddPipelineBehaviors();

        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IGmailServiceClient, GmailServiceClient>();
    }

    private static void AddAutoMapper(this IServiceCollection services)
    {
        var profiles = new Profile[]
        {
            new MappingEmail()
        };

        var mapper = new MapperConfiguration(options => options.AddProfiles(profiles)).CreateMapper();

        services.AddSingleton(mapper);
    }

    /// <summary>
    /// If it has not already been registered. This ensures that the validation pipeline behavior is only registered once to avoid redundant registrations.
    /// </summary>
    /// <param name="services">The service collection to add the pipeline behavior to.</param>
    private static void AddPipelineBehaviors(this IServiceCollection services)
    {
        if (!services.Any(service => service.ServiceType == typeof(IPipelineBehavior<,>) && service.ImplementationType == typeof(ValidationBehavior<,>)))
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}