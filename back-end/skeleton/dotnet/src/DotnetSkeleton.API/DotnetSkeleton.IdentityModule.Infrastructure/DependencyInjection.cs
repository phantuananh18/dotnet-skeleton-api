using DotnetSkeleton.IdentityModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.IdentityModule.Infrastructure.DbContexts;
using DotnetSkeleton.IdentityModule.Infrastructure.Repositories.MySQL;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetSkeleton.IdentityModule.Infrastructure
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds the necessary infrastructure services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        public static void AddIdentityModuleInfrastructure(this IServiceCollection services)
        {
            services.AddDbContexts();
            services.AddRepositories();
        }

        /// <summary>
        /// Adds the database contexts to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the contexts to.</param>
        public static void AddDbContexts(this IServiceCollection services)
        {
            services.AddDbContext<SkeletonDbContext>();
        }

        /// <summary>
        /// Adds the repositories to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the repositories to.</param>
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserAccountRepository, UserAccountRepository>();
        }
    }
}