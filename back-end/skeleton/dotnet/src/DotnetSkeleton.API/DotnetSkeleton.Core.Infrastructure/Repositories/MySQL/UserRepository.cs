using DotnetSkeleton.Core.Domain.Entities.MySQLEntities;
using DotnetSkeleton.Core.Domain.Interfaces.Repositories;
using DotnetSkeleton.Core.Infrastructure.DbContexts;
using DotnetSkeleton.SharedKernel.Utils.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetSkeleton.Core.Infrastructure.Repositories.MySQL
{
    /// <summary>
    /// User repository
    /// </summary>
    public class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        private readonly SkeletonDbContext _context;

        public UserRepository(SkeletonDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserProfileData?> GetUserProfileDataByIdAsync(int userId)
        {
            var users = _context.Users;
            var roles = _context.Roles;
            return await (from u in users
                          join r in roles on u.RoleId equals r.RoleId
                          where u.UserId == userId
                          select new UserProfileData()
                          {
                              UserId = u.UserId,
                              Email = u.Email,
                              FirstName = u.FirstName,
                              LastName = u.LastName,
                              MobilePhone = u.MobilePhone,
                              RoleId = r.RoleId,
                              RoleName = r.Name
                          }).FirstOrDefaultAsync();
        }
    }
}