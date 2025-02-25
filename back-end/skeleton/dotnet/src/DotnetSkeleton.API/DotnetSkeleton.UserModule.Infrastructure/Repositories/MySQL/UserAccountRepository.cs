using DotnetSkeleton.UserModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.UserModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.UserModule.Infrastructure.DbContexts;

namespace DotnetSkeleton.UserModule.Infrastructure.Repositories.MySQL
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
