using DotnetSkeleton.Utils.RabbitMqService.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetSkeleton.Utils.RabbitMqService;

public static class DependencyInjection
{
    public static void AddRabbitMqService(this IServiceCollection services)
    {
        services.AddSingleton(typeof(IRabbitMqService<>), typeof(Services.RabbitMqService<>));
    }
}