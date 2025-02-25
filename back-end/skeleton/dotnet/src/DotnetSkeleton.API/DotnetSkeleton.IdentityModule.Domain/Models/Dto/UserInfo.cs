using DotnetSkeleton.IdentityModule.Domain.Entities.MySQLEntities;

namespace DotnetSkeleton.IdentityModule.Domain.Models.Dto
{
    public class UserInfo
    {
        public User? User { get; set; }
        public Role? Role { get; set; }
        public List<UserAccount>? UserAccount { get; set; }
    }
}