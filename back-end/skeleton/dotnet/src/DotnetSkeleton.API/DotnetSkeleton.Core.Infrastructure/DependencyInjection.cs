using DotnetSkeleton.Core.Domain.Interfaces.Repositories;
using DotnetSkeleton.Core.Infrastructure.DbContexts;
using DotnetSkeleton.Core.Infrastructure.Repositories.MySQL;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetSkeleton.Core.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Adds the necessary infrastructure services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    public static void AddCoreModuleInfrastructure(this IServiceCollection services)
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
        services.AddDbContext<SkeletonMongoDbContext>();
    }

    /// <summary>
    /// Adds the repositories to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the repositories to.</param>
    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
    }
}