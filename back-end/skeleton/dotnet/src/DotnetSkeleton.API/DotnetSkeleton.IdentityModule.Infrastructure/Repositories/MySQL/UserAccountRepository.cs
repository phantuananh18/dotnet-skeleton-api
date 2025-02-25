using DotnetSkeleton.IdentityModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.IdentityModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.IdentityModule.Infrastructure.DbContexts;

namespace DotnetSkeleton.IdentityModule.Infrastructure.Repositories.MySQL
{
    public class UserAccountRepository : BaseRepository<UserAccount, int>, IUserAccountRepository
    {
        private readonly SkeletonDbContext _context;

        public UserAccountRepository(SkeletonDbContext context) : base(context)
        {
            _context = context;
        }
    }
}