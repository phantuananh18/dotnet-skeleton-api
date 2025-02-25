namespace DotnetSkeleton.UserModule.Domain.Model.Dtos.Roles
{
    public class RolePaginationDto
    {
        public int RoleId { get; set; }
        public string? Role { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public int TotalRecords { get; set; }
    }
}
