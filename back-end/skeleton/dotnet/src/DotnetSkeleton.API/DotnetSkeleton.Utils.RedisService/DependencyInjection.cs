using DotnetSkeleton.Utils.RedisService.Interfaces;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetSkeleton.Utils.RedisService;

public static class DependencyInjection
{
    public static void AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        var redisOptions = configuration.GetSection(RedisOptions.JsonKey).Get<RedisOptions>()!;
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisOptions.ConnectionString;
        });

        services.AddScoped<IRedisService, Services.RedisService>();
    }
}