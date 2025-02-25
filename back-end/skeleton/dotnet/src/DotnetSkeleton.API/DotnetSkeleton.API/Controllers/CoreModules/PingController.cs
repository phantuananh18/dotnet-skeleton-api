using DotnetSkeleton.SharedKernel.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;

namespace DotnetSkeleton.API.Controllers.CoreModules;

/// <summary>
/// Controller for handling ping requests.
/// </summary>
[ApiController]
[Route("/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
public class PingController : ControllerBase
{
    /// <summary>
    /// Retrieves a response indicating that the server is alive.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> representing the HTTP response.</returns>        
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [EnableRateLimiting(Constant.SystemInfo.TokenBucketRateLimit)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Get()
    {
        return Ok("Pong");
    }
}