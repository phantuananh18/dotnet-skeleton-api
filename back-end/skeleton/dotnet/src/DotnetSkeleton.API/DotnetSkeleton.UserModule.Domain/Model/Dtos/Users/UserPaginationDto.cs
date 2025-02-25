namespace DotnetSkeleton.UserModule.Domain.Model.Dtos.Users
{
    public class UserPaginationDto
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public required string Email { get; set; }
        public string? MobilePhone { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public int RoleId { get; set; }
        public string? Role { get; set; }
        public bool IsDeleted { get; set; }
        public int TotalRecords { get; set; }
    }
}
