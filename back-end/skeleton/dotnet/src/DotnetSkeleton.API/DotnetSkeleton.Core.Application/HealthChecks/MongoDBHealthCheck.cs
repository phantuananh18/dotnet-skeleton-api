using DotnetSkeleton.Core.Infrastructure.DbContexts;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace DotnetSkeleton.Core.Application.HealthChecks;

public class MongoDBHealthCheck : IHealthCheck
{
    #region Private Fields
    private readonly SkeletonMongoDbContext _skeletonMongoDbContext;
    private readonly ILogger<MongoDBHealthCheck> _logger;

    #endregion

    #region Constructor
    public MongoDBHealthCheck(SkeletonMongoDbContext skeletonMongoDbContext, ILogger<MongoDBHealthCheck> logger)
    {
        _skeletonMongoDbContext = skeletonMongoDbContext;
        _logger = logger;
    }

    #endregion
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            var connectionState = await _skeletonMongoDbContext.Database.CanConnectAsync(cancellationToken)
                ? HealthStatus.Healthy
                : HealthStatus.Unhealthy;

            _logger.LogInformation($@"[MongoDBHealthCheck] The database connection state = {connectionState} at {DateTime.UtcNow}");
            return connectionState switch
            {
                HealthStatus.Healthy => HealthCheckResult.Healthy("MongoDB database connection is healthy"),
                HealthStatus.Unhealthy => HealthCheckResult.Unhealthy("MongoDB database connection is unhealthy"),
                _ => HealthCheckResult.Unhealthy("An unhealthy result.")
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($@"[MongoDBHealthCheck] An error occurred while checking the database connection. Exception: {ex}");
            return HealthCheckResult.Unhealthy("An error occurred while checking the database connection");
        }
    }
}