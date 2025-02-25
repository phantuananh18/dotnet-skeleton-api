using DotnetSkeleton.NotificationModule.Application.Services;
using DotnetSkeleton.NotificationModule.Domain.Interfaces.Services;
using DotnetSkeleton.SharedKernel.Utils.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DotnetSkeleton.NotificationModule.Application
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds the Notification Module services and configuration to the service collection.
        /// </summary>
        /// <param name="services">The service collection to which the services will be added.</param>
        /// <param name="configuration">The application configuration to bind settings from.</param>
        public static void AddNotificationModuleApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices();
        }

        /// <summary>
        /// Configures services related to external services (e.g., Twilio) for the Notification Module.
        /// </summary>
        /// <param name="services"></param>
        private static void AddServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddPipelineBehaviors();
            services.AddSignalR(options => { options.KeepAliveInterval = TimeSpan.FromSeconds(5); }).AddMessagePackProtocol();

            services.AddScoped<INotificationService, NotificationService>();
        }

        /// <summary>
        /// Adds the <see cref="ValidationBehaviour{TRequest, TResponse}"/> to the service collection 
        /// if it has not already been registered. This ensures that the validation pipeline behavior is 
        /// only registered once to avoid redundant registrations.
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
