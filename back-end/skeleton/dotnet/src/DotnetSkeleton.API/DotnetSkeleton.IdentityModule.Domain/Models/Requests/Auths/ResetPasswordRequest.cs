namespace DotnetSkeleton.IdentityModule.Domain.Models.Requests.Auths
{
    public class ResetPasswordRequest
    {
        public required string NewPassword { get; set; }
    }
}