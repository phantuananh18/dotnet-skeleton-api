namespace DotnetSkeleton.UserModule.Domain.Model.Responses.PermissionResponses
{
    public class PermissionPaginationResponse
    {
        public int FeatureId { get; set; }
        public string? FeatureName { get; set; }
        public string? FeatureDescription { get; set; }
        public List<PermissionDetailResponse>? Permissions { get; set; }
    }

    public class PermissionDetailResponse
    {
        public int PermissionId { get; set; }
        public string? PermissionName { get; set; }
        public bool Allowed { get; set; }
    }
}