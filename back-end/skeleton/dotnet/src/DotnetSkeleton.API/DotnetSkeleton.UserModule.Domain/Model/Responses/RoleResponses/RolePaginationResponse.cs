namespace DotnetSkeleton.UserModule.Domain.Model.Responses.RoleResponses
{
    public class RolePaginationResponse
    {
        public int RoleId { get; set; }
        public string? Role { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
