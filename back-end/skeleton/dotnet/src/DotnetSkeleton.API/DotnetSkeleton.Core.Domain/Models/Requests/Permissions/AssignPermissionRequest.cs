namespace DotnetSkeleton.Core.Domain.Models.Requests.Permissions
{
    public class AssignPermissionRequest
    {
        public int PermissionId { get; set; }
        public bool Allowed { get; set; }
    }
}