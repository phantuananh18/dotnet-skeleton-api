namespace DotnetSkeleton.IdentityModule.Domain.Models.Requests.Users;

public class UpdateUserPayload
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MobilePhone { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? Role { get; set; }
    public UserAccountRequest? UserAccount { get; set; }
}