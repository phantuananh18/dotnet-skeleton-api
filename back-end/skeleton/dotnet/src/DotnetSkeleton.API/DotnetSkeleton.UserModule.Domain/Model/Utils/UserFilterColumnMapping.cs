namespace DotnetSkeleton.UserModule.Domain.Model.Utils
{
    public class UserFilterColumnMapping
    {
        public string Role { get; } = "r.Name";
        public string IsDeleted { get; } = "u.IsDeleted";
    }
}
