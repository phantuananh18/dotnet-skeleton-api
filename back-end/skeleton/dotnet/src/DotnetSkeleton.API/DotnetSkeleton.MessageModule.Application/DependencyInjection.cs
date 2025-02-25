using DotnetSkeleton.MessageModule.Application.HealthChecks;
using DotnetSkeleton.MessageModule.Application.Services;
using DotnetSkeleton.MessageModule.Domain.Interfaces.Services;
using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;

namespace DotnetSkeleton.MessageModule.Application
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds the Message Module services and configuration to the service collection.
        /// </summary>
        /// <param name="services">The service collection to which the services will be added.</param>
        /// <param name="configuration">The application configuration to bind settings from.</param>
        public static void AddMessageModuleApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices();
            services.ConfigureServices(configuration);
        }

        /// <summary>
        /// Adds services and behaviors related to the Message Module to the service collection.
        /// </summary>
        /// <param name="services">The service collection to which the services will be added.</param>
        public static void AddServices(this IServiceCollection services)
        {
            services.AddHealthChecks().AddCheck<MessagingHealthCheck>(Constant.SystemInfo.MessageModule);

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddPipelineBehaviors();

            services.AddSingleton(typeof(IMessagingService), typeof(MessagingService));
        }

        /// <summary>
        /// Configures services related to external services (e.g., Twilio) for the Message Module.
        /// </summary>
        /// <param name="services">The service collection to which the services will be added.</param>
        /// <param name="configuration">The application configuration to bind settings from.</param>
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TwilioOptions>(configuration.GetSection(TwilioOptions.JsonKey));
        }

        /// <summary>
        /// If it has not already been registered. This ensures that the validation pipeline behavior is only registered once to avoid redundant registrations.
        /// </summary>
        /// <param name="services">The service collection to add the pipeline behavior to.</param>
        public static void AddPipelineBehaviors(this IServiceCollection services)
        {
            if (!services.Any(service =>
                service.ServiceType == typeof(IPipelineBehavior<,>) &&
                service.ImplementationType == typeof(ValidationBehavior<,>)))
            {
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            }
        }
    }
}
