using DotnetSkeleton.Core.Infrastructure.DbContexts;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace DotnetSkeleton.Application.HealthChecks;

public class MySQLHealthCheck : IHealthCheck
{
    #region Private Fields
    private readonly SkeletonDbContext _skeletonDbContext;
    private readonly ILogger<MySQLHealthCheck> _logger;

    #endregion

    #region Constructor
    public MySQLHealthCheck(SkeletonDbContext skeletonDbContext, ILogger<MySQLHealthCheck> logger)
    {
        _skeletonDbContext = skeletonDbContext;
        _logger = logger;
    }

    #endregion

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            var connectionState = await _skeletonDbContext.Database.CanConnectAsync(cancellationToken)
                ? HealthStatus.Healthy
                : HealthStatus.Unhealthy;

            _logger.LogInformation($@"[MySQLHealthCheck] The database connection state = {connectionState} at {DateTime.UtcNow}");
            return connectionState switch
            {
                HealthStatus.Healthy => HealthCheckResult.Healthy("MySQL database connection is healthy"),
                HealthStatus.Unhealthy => HealthCheckResult.Unhealthy("MySQL database connection is unhealthy"),
                _ => HealthCheckResult.Unhealthy("An unhealthy result.")
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($@"[MySQLHealthCheck] An error occurred while checking the database connection. Exception: {ex}");
            return HealthCheckResult.Unhealthy("An error occurred while checking the database connection");
        }

    }
}