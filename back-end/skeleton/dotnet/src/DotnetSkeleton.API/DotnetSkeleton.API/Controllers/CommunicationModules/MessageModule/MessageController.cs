using DotnetSkeleton.Core.Domain.Interfaces.Services;
using DotnetSkeleton.Core.Domain.Models.Requests.Messages;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace DotnetSkeleton.API.Controllers.CommunicationModules.MessageModule;

[ApiController]
[ApiVersion("1")]
[Extensions.Authorization.Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
public class MessageController : TwilioController
{
    #region Private Fields
    private readonly IMessageService _messageService;
    private readonly ILogger<MessageController> _logger;

    #endregion

    #region Constructor
    public MessageController(IMessageService messageService, ILogger<MessageController> logger)
    {
        _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
        _logger = logger;
    }

    #endregion

    #region Post Methods
    // TO-DO: Implement post methods

    /// <summary>
    /// Handles the HTTP POST request to send an SMS message.
    /// </summary>
    /// <param name="request">The <see cref="SendSmsRequest"/> containing the phone number and message details.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> that represents the result of the SMS sending operation.
    /// The status code reflects the success or failure of the process.
    /// </returns>
    /// <remarks>
    /// This method receives a command with the required details for sending an SMS, such as the recipient's phone number and message.
    /// It forwards the command to the mediator for processing and returns the result, including an appropriate HTTP status code.
    /// </remarks>
    [HttpPost]
    [Route("send-sms")]
    public async Task<IActionResult> SendSmsAsync(SendSmsRequest request)
    {
        var result = await _messageService.SendSmsAsync(request);
        return StatusCode(result.Status, result);
    }

    /// <summary>
    /// Handles incoming SMS messages from Twilio. 
    /// This method processes the SMS content and logs the unique SMS message identifier (SmsMessageSid) and message body.
    /// It also responds to the sender with a thank you message using TwiML.
    /// </summary>
    /// <param name="smsMessageSid">The unique identifier for the SMS message, provided by Twilio (SmsMessageSid).</param>
    /// <param name="body">The content of the received SMS message (Twilio's 'Body' parameter).</param>
    /// <returns>
    /// A TwiMLResult that contains the Twilio Markup Language (TwiML) response, 
    /// which sends a thank you message back to the sender.
    /// </returns>
    [HttpPost]
    [Route("receive-sms")]
    public TwiMLResult ReceiveSms([FromForm] string smsMessageSid, [FromForm] string body)
    {
        //Todo logic handle
        var messagingResponse = new MessagingResponse();
        _logger.LogInformation("Received SMS with SmsMessageSid: {smsMessageSid}", smsMessageSid);
        messagingResponse.Message("Thank you for your message!");

        return TwiML(messagingResponse);
    }

    /// <summary>
    /// Initiates a verification process for the current user via the specified communication channel.
    /// </summary>
    /// <param name="request">The request containing the channel to be used for verification.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> that represents the result of the verification initiation request.
    /// The status code corresponds to the outcome of the verification process.
    /// </returns>
    /// <remarks>
    /// This method retrieves the current user's data from the HTTP context, initiates a verification process 
    /// using the provided mobile phone number and channel, and returns the result. 
    /// The response status code will reflect the success or failure of the operation.
    /// </remarks>
    [HttpPost]
    [Route("start-verification")]
    public async Task<IActionResult> StartVerificationAsync([FromBody] StartVerificationRequest request)
    {
        var result = await _messageService.StartVerificationAsync(request);
        return StatusCode(result.Status, result);
    }

    /// <summary>
    /// Verifies the provided code for the current user's mobile phone number.
    /// </summary>
    /// <param name="request">The request containing the verification code to be checked.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> that represents the result of the verification check.
    /// The status code corresponds to the outcome of the verification process.
    /// </returns>
    /// <remarks>
    /// This method retrieves the current user's data from the HTTP context and attempts to verify the provided code 
    /// against the user's mobile phone number. The response status code will indicate whether the verification 
    /// was successful or if it failed.
    /// </remarks>
    [HttpPost]
    [Route("check-verification")]
    public async Task<IActionResult> CheckVerificationAsync([FromBody] CheckVerificationRequest request)
    {
        var result = await _messageService.CheckVerificationAsync(request);
        return StatusCode(result.Status, result);
    }

    #endregion
}