using Asp.Versioning;
using DotnetSkeleton.EmailModule.Application.Commands.QueueSendOutgoingEmailCommand;
using DotnetSkeleton.EmailModule.Application.Commands.SendOutgoingEmailCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotnetSkeleton.EmailModule.API.Controllers;

[ApiController]
[Route("/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
public class EmailsController : ControllerBase
{
    #region Private Fields
    // TO-DO: Add private fields
    private readonly IMediator _mediator;

    #endregion

    #region Constructor
    public EmailsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region GET Methods
    // TO-DO: Implement GET methods

    #endregion

    #region POST Methods
    // TO-DO: Implement POST methods

    [HttpPost]
    [Route("send-out")]
    public async Task<IActionResult> SendOutgoingEmail([FromBody] SendOutgoingEmailCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.Status, result);
    }

    [HttpPost]
    [Route("send-out/queue")]
    public async Task<IActionResult> QueueOutgoingEmail([FromBody] QueueSendOutgoingEmailCommand command)
    {
        var result = await _mediator.Send(command);
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