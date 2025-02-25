using DotnetSkeleton.UserModule.Domain.Model.Requests.Permissions;

namespace DotnetSkeleton.UserModule.Domain.Model.Requests.RolePermissions
{
    public class AssignRolePermissionRequest
    {
        public int RoleId { get; set; }

        public required List<AssignPermissionRequest> Permissions { get; set; }
    }
}
