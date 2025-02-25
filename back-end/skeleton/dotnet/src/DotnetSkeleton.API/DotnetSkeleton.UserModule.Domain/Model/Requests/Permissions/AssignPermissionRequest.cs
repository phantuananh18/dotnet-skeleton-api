namespace DotnetSkeleton.UserModule.Domain.Model.Requests.Permissions
{
    public class AssignPermissionRequest
    {
        public int PermissionId { get; set; }
        public bool Allowed { get; set; }
    }
}