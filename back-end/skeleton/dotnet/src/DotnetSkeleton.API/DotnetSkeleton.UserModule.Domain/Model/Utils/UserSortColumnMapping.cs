namespace DotnetSkeleton.UserModule.Domain.Model.Utils
{
    public class UserSortColumnMapping
    {
        public string FirstName { get; } = "u.FirstName";
        public string LastName { get; } = "u.LastName";
        public string Email { get; } = "u.Email";
        public string Phone { get; } = "u.MobilePhone";
    }
}
