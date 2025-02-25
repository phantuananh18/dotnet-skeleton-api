using DotnetSkeleton.SharedKernel.Utils;

namespace DotnetSkeleton.Core.Domain.Models.Responses.HealthCheck;

public class HealthCheckResponse
{
    public Constant.HealthCheckStatus GlobalStatus
    {
        get
        {
            if (Results is null or { Count: 0 })
            {
                return Constant.HealthCheckStatus.NotApplicable;
            }

            var maxStatus = Results.Values
                .Select(r => r.Status)
                .Where(s => s != Constant.HealthCheckStatus.NotApplicable)
                .ToList();

            return !maxStatus.Any() ? Constant.HealthCheckStatus.Good : maxStatus.Max();
        }
    }

    public Dictionary<string, HealthCheckStatusDetail>? Results { get; set; }
}

public class HealthCheckStatusDetail
{
    public Constant.HealthCheckStatus Status { get; set; }
    public string? Description { get; set; }
}