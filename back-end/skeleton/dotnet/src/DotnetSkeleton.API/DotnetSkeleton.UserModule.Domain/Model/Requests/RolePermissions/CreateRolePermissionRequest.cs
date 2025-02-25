namespace DotnetSkeleton.UserModule.Domain.Model.Requests.RolePermissions
{
    public class CreateRolePermissionRequest
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
    }
}
