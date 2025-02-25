namespace DotnetSkeleton.SharedKernel.Utils.Models;

public class UserProfileData
{
    public int UserId { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MobilePhone { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public int RoleId { get; set; }
    public required string RoleName { get; set; }
}