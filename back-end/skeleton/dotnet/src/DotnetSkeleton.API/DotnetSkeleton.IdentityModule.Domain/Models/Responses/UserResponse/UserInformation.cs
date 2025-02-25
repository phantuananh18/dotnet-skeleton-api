using System.Text.Json.Serialization;

namespace DotnetSkeleton.IdentityModule.Domain.Models.Responses.UserResponse;

public class UserInformation
{
    [JsonPropertyName("userId")]
    public int UserId { get; set; }

    [JsonPropertyName("email")]
    public required string Email { get; set; }

    [JsonPropertyName("firstName")]
    public string? FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string? LastName { get; set; }

    [JsonPropertyName("mobilePhone")]
    public string? MobilePhone { get; set; }

    [JsonPropertyName("role")]
    public string? Role { get; set; }

    [JsonPropertyName("profilePictureUrl")]
    public string? ProfilePictureUrl { get; set; }
}