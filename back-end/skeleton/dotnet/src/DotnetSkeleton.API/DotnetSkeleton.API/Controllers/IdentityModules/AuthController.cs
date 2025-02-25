using DotnetSkeleton.Core.Domain.Interfaces.Services;
using DotnetSkeleton.Core.Domain.Models.Requests.Auths;
using Microsoft.AspNetCore.Authorization;

namespace DotnetSkeleton.API.Controllers.IdentityModules
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
        private readonly IAuthService _authService;

        #endregion

        #region Constructor
        public AuthController(IAuthService authService)
        {
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
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest signUpRequest)
        {
            var result = await _authService.SignUp(signUpRequest);
            return StatusCode(result.Status, result);
        }

        /// <summary>
        /// Signs in a user with the provided credentials.
        /// </summary>
        /// <param name="signInRequest"><see cref="SignInRequest"/> The credentials of the user to authenticate.</param>
        /// <returns>An action result representing the result of the authentication process, including access and refresh tokens upon successful authentication.</returns>
        [HttpPost]
        [Route("sign-in")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest signInRequest)
        {
            var result = await _authService.SignIn(signInRequest);
            return StatusCode(result.Status, result);
        }

        /// <summary>
        /// Generates an access token from the provided refresh token.
        /// </summary>
        /// <param name="refreshTokenRequest">The refresh token used to generate the access token.</param>
        /// <returns>An action result that includes the access token.</returns>
        [HttpPost]
        [Route("refresh-token")]
        [Extensions.Authorization.Authorize]
        public async Task<IActionResult> RefreshAccessToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            var result = await _authService.RefreshAccessToken(refreshTokenRequest);
            return StatusCode(result.Status, result);
        }

        /// <summary>
        /// Initiates the process for resetting a user's password by sending a password reset email to the provided email address.
        /// </summary>
        /// <param name="forgotPasswordRequest"><see cref="ForgotPasswordRequest"/> The user's email address.</param>
        /// <returns>An action result indicating the outcome of the password reset request.</returns>
        [HttpPost]
        [Route("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest forgotPasswordRequest)
        {
            var result = await _authService.ForgotPassword(forgotPasswordRequest);
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
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string token, [FromBody] ResetPasswordRequest resetPasswordRequest)
        {
            var result = await _authService.ResetPassword(token, resetPasswordRequest);
            return StatusCode(result.Status, result);
        }

        /// <summary>
        /// GitHub OAuth callback process.
        /// </summary>
        /// <param name="request">The SignUp request used for OAuth call back request</param>
        /// <returns>The base response represent for the task result of callback process</returns>
        [HttpPost]
        [Route("oauth-callback")]
        [AllowAnonymous]
        public async Task<IActionResult> OAuthCallBack([FromBody] OAuthCallBackRequest request)
        {
            var result = await _authService.OAuthCallBack(request);
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
