namespace DotnetSkeleton.UserModule.Domain.Model.Requests.Permissions
{
    public class CreatePermissionRequest
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public required string Description { get; set; }
        public int FeatureId { get; set; }
    }
}