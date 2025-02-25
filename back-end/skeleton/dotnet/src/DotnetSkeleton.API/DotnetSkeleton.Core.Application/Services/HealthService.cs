using DotnetSkeleton.Core.Domain.Interfaces.Services;
using DotnetSkeleton.Core.Domain.Models.Responses.HealthCheck;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DotnetSkeleton.Core.Application.Services;

public class HealthService : IHealthService
{
    #region Private Fields
    private readonly HealthCheckService _healthCheckService;

    #endregion

    #region Constructor
    public HealthService(HealthCheckService healthCheckService)
    {
        _healthCheckService = healthCheckService;
    }

    #endregion

    #region Public Methods
    // TO-DO: Implement public methods

    public async Task<BaseResponse> CheckHealthAsync()
    {
        var healthCheckResult = await _healthCheckService.CheckHealthAsync();
        var response = new HealthCheckResponse
        {
            Results = healthCheckResult.Entries.ToDictionary(
                h => h.Key,
                h => new HealthCheckStatusDetail
                {
                    Status = ClassifyHealthStatus(h.Value.Status),
                    Description = h.Value.Description
                })
        };

        return BaseResponse.Ok(response);
    }

    #endregion

    #region Private Methods
    // TO-DO: Implement private methods

    private static DotnetSkeleton.SharedKernel.Utils.Constant.HealthCheckStatus ClassifyHealthStatus(HealthStatus status)
    {
        return status switch
        {
            HealthStatus.Healthy => SharedKernel.Utils.Constant.HealthCheckStatus.Good,
            HealthStatus.Degraded => SharedKernel.Utils.Constant.HealthCheckStatus.Warning,
            HealthStatus.Unhealthy => SharedKernel.Utils.Constant.HealthCheckStatus.Critical,
            _ => SharedKernel.Utils.Constant.HealthCheckStatus.NotApplicable
        };
    }

    #endregion
}