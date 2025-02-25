using AutoMapper;
using DotnetSkeleton.IdentityModule.Application.Mappings;
using DotnetSkeleton.IdentityModule.Application.Services;
using DotnetSkeleton.IdentityModule.Domain.Interfaces.Services;
using DotnetSkeleton.SharedKernel.Utils.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;
using System.Reflection;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using Microsoft.Extensions.Configuration;

namespace DotnetSkeleton.IdentityModule.Application
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds the Identity Module services and configuration to the service collection.
        /// </summary>
        /// <param name="services">The service collection to which the services will be added.</param>
        /// <param name="configuration">The application configuration to bind settings from.</param>
        public static void AddIdentityModuleApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices(configuration);
        }

        /// <summary>
        /// Adds services to the service collection.
        /// </summary>
        private static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddHttpContextAccessor();
            services.AddAutoMapper();
            services.AddPipelineBehaviors();

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

            // Add services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
        }

        /// <summary>
        /// Adds AutoMapper to the service collection.
        /// </summary>
        private static void AddAutoMapper(this IServiceCollection services)
        {
            var profiles = new Profile[]
            {
                new MappingAuth(),
                new MappingUser()
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