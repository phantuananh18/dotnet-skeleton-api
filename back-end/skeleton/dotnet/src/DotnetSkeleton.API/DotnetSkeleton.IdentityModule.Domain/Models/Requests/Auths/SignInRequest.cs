namespace DotnetSkeleton.IdentityModule.Domain.Models.Requests.Auths
{
    public class SignInRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}