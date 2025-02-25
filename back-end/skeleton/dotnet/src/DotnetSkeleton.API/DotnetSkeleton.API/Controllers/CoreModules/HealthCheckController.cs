using DotnetSkeleton.Core.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace DotnetSkeleton.API.Controllers.CoreModules;

/// <summary>
/// Controller for handling health check requests.
/// </summary>
[ApiController]
[Route("/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[AllowAnonymous]
public class HealthCheckController : ControllerBase
{
    private readonly IHealthService _healthService;

    public HealthCheckController(IHealthService healthService)
    {
        _healthService = healthService;
    }

    [HttpGet]
    public async Task<IActionResult> GetHealthCheck()
    {
        var result = await _healthService.CheckHealthAsync();
        return StatusCode(result.Status, result);
    }
}