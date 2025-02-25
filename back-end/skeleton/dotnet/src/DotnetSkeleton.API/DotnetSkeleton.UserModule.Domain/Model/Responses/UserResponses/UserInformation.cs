namespace DotnetSkeleton.UserModule.Domain.Model.Responses.UserResponses;

public class UserInformation
{
    public int UserId { get; set; }
    public string? Username { get; set; }
    public required string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MobilePhone { get; set; }
    public string? Role { get; set; }
    public string? ProfilePictureUrl { get; set; }
}