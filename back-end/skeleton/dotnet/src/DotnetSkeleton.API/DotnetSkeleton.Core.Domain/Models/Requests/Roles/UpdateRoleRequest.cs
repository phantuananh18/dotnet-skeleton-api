namespace DotnetSkeleton.Core.Domain.Models.Requests.Roles
{
    public class UpdateRoleRequest
    {
        public int RoleId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}