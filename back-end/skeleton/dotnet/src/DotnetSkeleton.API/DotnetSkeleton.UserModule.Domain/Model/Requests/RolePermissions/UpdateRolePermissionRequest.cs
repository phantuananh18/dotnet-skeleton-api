namespace DotnetSkeleton.UserModule.Domain.Model.Requests.RolePermissions
{
    public class UpdateRolePermissionRequest
    {
        public int RolePermissionId { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
    }
}