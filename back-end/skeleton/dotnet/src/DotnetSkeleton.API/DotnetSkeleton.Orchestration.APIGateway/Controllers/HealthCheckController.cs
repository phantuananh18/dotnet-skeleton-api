using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetSkeleton.Orchestration.APIGateway.Controllers;

[ApiController]
[Route("/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[AllowAnonymous]
public class HealthCheckController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetHealthCheck()
    {
        return await new Task<IActionResult>(null);
    }
}