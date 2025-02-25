using DotnetSkeleton.UserModule.Domain.Entities.MySQLEntities;

namespace DotnetSkeleton.UserModule.Domain.Model.Dtos.Users;

public class UserInfo
{
    public required User User { get; set; }

    public required Role Role { get; set; }

    public List<UserAccount>? UserAccount { get; set; }
}