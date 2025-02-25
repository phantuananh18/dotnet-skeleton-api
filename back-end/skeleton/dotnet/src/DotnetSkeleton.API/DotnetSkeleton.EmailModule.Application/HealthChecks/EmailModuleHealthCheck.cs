using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DotnetSkeleton.EmailModule.Application.HealthChecks;

public class EmailModuleHealthCheck : IHealthCheck
{
    private readonly ILogger<EmailModuleHealthCheck> _logger;

    public EmailModuleHealthCheck(ILogger<EmailModuleHealthCheck> logger)
    {
        _logger = logger;
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        _logger.LogInformation($@"[EmailModuleHealthCheck] The email module is healthy at {DateTime.UtcNow}");
        return Task.FromResult(HealthCheckResult.Healthy("Email module is healthy"));
    }
}