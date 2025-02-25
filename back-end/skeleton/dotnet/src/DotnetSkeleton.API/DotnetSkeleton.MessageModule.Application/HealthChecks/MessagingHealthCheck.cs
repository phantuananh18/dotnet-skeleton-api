using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace DotnetSkeleton.MessageModule.Application.HealthChecks
{
    public class MessagingHealthCheck : IHealthCheck
    {
        private readonly ILogger<MessagingHealthCheck> _logger;

        public MessagingHealthCheck(ILogger<MessagingHealthCheck> logger)
        {
            _logger = logger;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            _logger.LogInformation($@"[MessagingHealthCheck] The email module is healthy at {DateTime.UtcNow}");
            return Task.FromResult(HealthCheckResult.Healthy("Message module is healthy"));
        }
    }
}
