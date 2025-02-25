using DotnetSkeleton.Core.Domain.Models.Requests.Permissions;

namespace DotnetSkeleton.Core.Domain.Models.Requests.RolePermissions
{
    public class AssignRolePermissionRequest
    {
        public int RoleId { get; set; }
        public List<AssignPermissionRequest> Permissions { get; set; }
    }
}