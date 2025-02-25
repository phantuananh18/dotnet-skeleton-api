using DotnetSkeleton.EmailModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.EmailModule.Infrastructure.DbContexts;
using DotnetSkeleton.EmailModule.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetSkeleton.EmailModule.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Adds the necessary infrastructure services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    public static void AddEmailModuleInfrastructure(this IServiceCollection services)
    {
        services.AddDbContexts();
        services.AddRepositories();
    }

    /// <summary>
    /// Adds the database contexts to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the contexts to.</param>
    private static void AddDbContexts(this IServiceCollection services)
    {
        services.AddDbContext<SkeletonDbContext>();
    }

    /// <summary>
    /// Adds the repositories to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the repositories to.</param>
    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICommunicationTemplateRepository, CommunicationTemplateRepository>();
        services.AddScoped<ICommunicationRepository, CommunicationRepository>();
        services.AddScoped<IEmailMetadataRepository, EmailMetadataRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}