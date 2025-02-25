namespace DotnetSkeleton.Core.Domain.Models.Requests.Permissions
{
    public class UpdatePermissionRequest
    {
        public int PermissionId { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public required string Description { get; set; }
    }
}