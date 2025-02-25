using DotnetSkeleton.IdentityModule.Domain.Entities.MySQLEntities;

namespace DotnetSkeleton.IdentityModule.Domain.Interfaces.Repositories
{
    public interface IUserAccountRepository : IBaseRepository<UserAccount, int>
    {
    }
}