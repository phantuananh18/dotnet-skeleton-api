namespace DotnetSkeleton.UserModule.Domain.Model.Dtos.Permission
{
    public class PermissionPaginationDto
    {
        public int FeatureId { get; set; }
        public string? FeatureName { get; set; }
        public string? FeatureDescription { get; set; }
        public int PermissionId { get; set; }
        public string? PermissionName { get; set; }
        public bool Allow { get; set; }
    }
}