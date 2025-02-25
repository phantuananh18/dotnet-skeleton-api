namespace DotnetSkeleton.UserModule.Domain.Model.Requests.Permissions
{
    public class UpdatePermissionRequest
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public required string Description { get; set; }
    }
}