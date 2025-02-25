using AutoMapper;
using DotnetSkeleton.SharedKernel.Utils.Behaviors;
using DotnetSkeleton.UserModule.Application.Mappings;
using DotnetSkeleton.UserModule.Application.Services;
using DotnetSkeleton.UserModule.Domain.Interfaces.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DotnetSkeleton.UserModule.Application
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds the User Module services and configuration to the service collection.
        /// </summary>
        /// <param name="services">The service collection to which the services will be added.</param>
        /// <param name="configuration">The application configuration to bind settings from.</param>
        public static void AddUserModuleApplication(this IServiceCollection services)
        {
            services.AddServices();
        }

        /// <summary>
        /// Configures services related to external services (e.g., Twilio) for the User Module.
        /// </summary>
        /// <param name="services"></param>
        private static void AddServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddPipelineBehaviors();
            services.AddAutoMapper();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IRolePermissionService, RolePermissionService>();
        }

        private static void AddAutoMapper(this IServiceCollection services)
        {
            var profiles = new Profile[]
            {
                new PermissionMapping(),
                new RoleMapping(),
                new RolePermissionMapping(),
                new UserMapping()
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
}
