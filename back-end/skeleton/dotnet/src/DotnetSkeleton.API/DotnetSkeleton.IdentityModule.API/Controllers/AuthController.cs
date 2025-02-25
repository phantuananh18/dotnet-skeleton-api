using Asp.Versioning;
using DotnetSkeleton.IdentityModule.Application.Commands.ForgotPasswordCommand;
using DotnetSkeleton.IdentityModule.Application.Commands.OAuthCallBackCommand;
using DotnetSkeleton.IdentityModule.Application.Commands.RefreshTokenCommand;
using DotnetSkeleton.IdentityModule.Application.Commands.ResetPasswordCommand;
using DotnetSkeleton.IdentityModule.Application.Commands.SignInCommand;
using DotnetSkeleton.IdentityModule.Application.Commands.SignUpCommand;
using DotnetSkeleton.IdentityModule.Domain.Interfaces.Services;
using DotnetSkeleton.IdentityModule.Domain.Models.Requests.Auths;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotnetSkeleton.IdentityModule.API.Controllers
{
    /// <summary>
    /// Identity Controller
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        #region Private Fields

        private readonly IMediator _mediator;
        private readonly IAuthService _authService;

        #endregion

        #region Constructor

        public AuthController(IMediator mediator, IAuthService authService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        #endregion

        #region GET method

        // TO-DO: Implement GET methods

        #endregion

        #region POST method

        /// <summary>
        /// Signs up a new user with the provided information.
        /// </summary>
        /// <param name="signUpRequest"><see cref="SignUpRequest"/> The details of the user to register.</param>
        /// <returns>An action result representing the result of the user registration process.</returns>
        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpCommand signUpRequest)
        {
            var result = await _mediator.Send(signUpRequest);
            return StatusCode(result.Status, result);
        }

        /// <summary>
        /// Signs in a user with the provided credentials.
        /// </summary>
        /// <param name="signInRequest"><see cref="SignInRequest"/> The credentials of the user to authenticate.</param>
        /// <returns>An action result representing the result of the authentication process, including access and refresh tokens upon successful authentication.</returns>
        [HttpPost]
        [Route("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInCommand signInRequest)
        {
            var result = await _mediator.Send(signInRequest);
            return StatusCode(result.Status, result);
        }

        /// <summary>
        /// Generates an access token from the provided refresh token.
        /// </summary>
        /// <param name="refreshTokenRequest">The refresh token used to generate the access token.</param>
        /// <returns>An action result that includes the access token.</returns>
        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshAccessToken([FromBody] RefreshTokenCommand refreshTokenRequest)
        {
            var result = await _mediator.Send(refreshTokenRequest);
            return StatusCode(result.Status, result);
        }

        /// <summary>
        /// Initiates the process for resetting a user's password by sending a password reset email to the provided email address.
        /// </summary>
        /// <param name="forgotPasswordRequest"><see cref="ForgotPasswordCommand"/> The user's email address.</param>
        /// <returns>An action result indicating the outcome of the password reset request.</returns>
        [HttpPost]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand forgotPasswordRequest)
        {
            var result = await _mediator.Send(forgotPasswordRequest);
            return StatusCode(result.Status, result);
        }

        /// <summary>
        /// Resets a user's password based on the provided reset password request and token.
        /// </summary>
        /// <param name="token">The token associated with the password reset request.</param>
        /// <param name="resetPasswordRequest"><see cref="ResetPasswordRequest"/> The new password for reset.</param>
        /// <returns>An action result indicating the outcome of the password reset operation.</returns>
        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword(string token, [FromBody] ResetPasswordRequest resetPasswordRequest)
        {
            var request = new ResetPasswordCommand()
            {
                Token = token,
                NewPassword = resetPasswordRequest.NewPassword
            };

            var result = await _mediator.Send(request);
            return StatusCode(result.Status, result);
        }

        /// <summary>
        /// OAuth callback process.
        /// </summary>
        /// <param name="command">The SignUp request used for OAuth call back request</param>
        /// <returns>The base response represent for the task result of callback process</returns>
        [HttpPost]
        [Route("oauth-callback")]
        public async Task<IActionResult> OAuthCallBack([FromBody] OAuthCallBackCommand command)
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
}