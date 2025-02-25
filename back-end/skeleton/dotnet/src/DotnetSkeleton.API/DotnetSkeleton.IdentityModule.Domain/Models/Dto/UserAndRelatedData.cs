using DotnetSkeleton.IdentityModule.Domain.Entities.MySQLEntities;

namespace DotnetSkeleton.IdentityModule.Domain.Models.Dto
{
    public class UserAndRelatedData
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public required string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MobilePhone { get; set; }
        public bool IsDeleted { get; set; }
        public Role? Role { get; set; }
    }
}