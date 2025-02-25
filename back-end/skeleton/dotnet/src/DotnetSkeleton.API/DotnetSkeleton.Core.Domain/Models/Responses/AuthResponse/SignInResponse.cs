namespace DotnetSkeleton.Core.Domain.Models.Responses.AuthResponse
{
    public class SignInResponse
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public required UserInformation UserInformation { get; set; }
    }

    public class UserInformation
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public required string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MobilePhone { get; set; }
        public string? Role { get; set; }
    }
}