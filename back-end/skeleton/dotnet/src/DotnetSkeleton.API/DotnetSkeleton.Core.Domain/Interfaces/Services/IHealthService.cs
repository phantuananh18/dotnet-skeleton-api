using DotnetSkeleton.SharedKernel.Utils.Models.Responses;

namespace DotnetSkeleton.Core.Domain.Interfaces.Services;

public interface IHealthService
{
    Task<BaseResponse> CheckHealthAsync();
}