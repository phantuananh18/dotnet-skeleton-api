using DotnetSkeleton.NotificationModule.Application.Commands.NotificationMessageCommand;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace DotnetSkeleton.API.Controllers.NotificationModules
{
    [Extensions.Authorization.Authorize]
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class NotificationsController : ControllerBase
    {
        #region Private Fields
        private readonly IMediator _mediator;

        public NotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Post Methods
        /// <summary>
        /// Handles the HTTP POST request to send a notification.
        /// </summary>
        /// <param name="command">The <see cref="SendNotificationCommand"/> containing the phone number and message details.</param>
        /// <returns>An <see cref="IActionResult"/> that represents the result of the Notification sending operation. The status code reflects the success or failure of the process.</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("send")]
        public async Task<IActionResult> SendNotification([FromBody] SendNotificationCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode(result.Status, result);
        }

        #endregion
    }
}