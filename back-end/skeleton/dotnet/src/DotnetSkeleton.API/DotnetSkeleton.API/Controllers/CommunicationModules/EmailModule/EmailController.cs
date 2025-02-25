using DotnetSkeleton.API.Extensions.Authorization;
using DotnetSkeleton.Core.Domain.Interfaces.Services;
using DotnetSkeleton.Core.Domain.Models.Requests.EmailRequests;

namespace DotnetSkeleton.API.Controllers.CommunicationModules.EmailModule;

[ApiController]
[Route("/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[Authorize]
public class EmailController : ControllerBase
{
    #region Private Fields
    // TO-DO: Add private fields
    private readonly IEmailService _emailService;

    #endregion

    #region Constructor
    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    #endregion

    #region GET Methods
    // TO-DO: Implement GET methods

    #endregion

    #region POST Methods
    // TO-DO: Implement POST methods

    /// <summary>
    /// Processes and sends an outgoing email. Validates the request, saves communication data, sends the email, and updates the status.
    /// </summary>
    /// <param name="request">The details of the outgoing email to be sent.</param>
    /// <returns>An <see cref="IActionResult"/> representing the HTTP response with the result and status.</returns>
    [HttpPost]
    [Route("send-out")]
    public async Task<IActionResult> SendOutgoingEmail([FromBody] OutgoingEmailRequest request)
    {
        var result = await _emailService.SendOutgoingEmail(request);
        return StatusCode(result.Status, result);
    }

    /// <summary>
    /// Sends an email based on the provided email request to queue.
    /// </summary>
    /// <param name="request">The details of the outgoing email to be queued.</param>
    /// <returns>An <see cref="IActionResult"/> representing the HTTP response with the result and status.</returns>
    [HttpPost]
    [Route("send-out/queue")]
    public async Task<IActionResult> QueueOutgoingEmail([FromBody] OutgoingEmailRequest request)
    {
        var result = await _emailService.QueueOutgoingEmail(request);
        return StatusCode(result.Status, result);
    }

    #endregion

    #region PUT Methods
    // TO-DO: Implement PUT methods

    #endregion

    #region DELETE Methods
    // TO-DO: Implement DELETE methods

    #endregion

    #region PATCH Methods
    // TO-DO: Implement PATCH methods

    #endregion
}