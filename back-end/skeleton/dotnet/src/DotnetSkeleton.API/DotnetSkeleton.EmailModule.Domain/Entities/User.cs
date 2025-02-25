using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetSkeleton.EmailModule.Domain.Entities;

/// <summary>
/// User entity
/// </summary>
[Table(nameof(User))]
public class User : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public required string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MobilePhone { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public int? RoleId { get; set; }
}