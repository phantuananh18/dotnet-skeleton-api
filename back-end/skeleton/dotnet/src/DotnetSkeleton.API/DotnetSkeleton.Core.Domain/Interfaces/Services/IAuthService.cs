using DotnetSkeleton.Core.Domain.Models.Requests.Auths;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;

namespace DotnetSkeleton.Core.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Generates an access token from the provided refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token used to generate the access token.</param>
        /// <returns>The task result contains an instance of <see cref="BaseResponse"/> representing the result of the access token generation process.</returns>
        Task<BaseResponse> RefreshAccessToken(RefreshTokenRequest refreshToken);

        /// <summary>
        /// Signs up a new user with the provided information.
        /// </summary>
        /// <param name="signUpRequest">An instance of the <see cref="SignUpRequest"/> class containing the details of the user to sign up.</param>
        /// <returns>An instance of the <see cref="BaseResponse"/> class representing the result of the user sign-up process.</returns>
        Task<BaseResponse> SignUp(SignUpRequest signUpRequest);

        /// <summary>
        /// Signs in a user with the provided credentials.
        /// </summary>
        /// <param name="signInRequest">An instance of the <see cref="SignInRequest"/> class containing the credentials of the user to authenticate.</param>
        /// <returns>An instance of the <see cref="BaseResponse"/> class representing the result of the authentication process, including access and refresh tokens upon successful authentication.</returns>
        Task<BaseResponse> SignIn(SignInRequest signInRequest);

        /// <summary>
        /// Initiates the process for resetting a user's password by sending a password reset email to the provided email address.
        /// </summary>
        /// <param name="forgotPasswordRequest">An instance of the <see cref="Microsoft.AspNetCore.Identity.Data.ForgotPasswordRequest"/> class containing the user's email address for initiating the password reset process.</param>
        /// <returns>An asynchronous task representing the outcome of the password reset request.</returns>
        Task<BaseResponse> ForgotPassword(ForgotPasswordRequest forgotPasswordRequest);

        /// <summary>
        /// Resets a user's password based on the provided reset password request and token.
        /// </summary>
        /// <param name="token">The token associated with the password reset request.</param>
        /// <param name="newPassword">The string containing the new password for reset.</param>
        /// <returns>An action result indicating the outcome of the password reset operation.</returns>
        Task<BaseResponse> ResetPassword(string token, ResetPasswordRequest newPassword);

        /// <summary>
        /// GitHub OAuth callback process.
        /// </summary>
        /// <param name="request">The SignUp request used for OAuth call back request</param>
        /// <returns>The base response represent for the task result of callback process</returns>
        Task<BaseResponse> OAuthCallBack(OAuthCallBackRequest request);
    }
}