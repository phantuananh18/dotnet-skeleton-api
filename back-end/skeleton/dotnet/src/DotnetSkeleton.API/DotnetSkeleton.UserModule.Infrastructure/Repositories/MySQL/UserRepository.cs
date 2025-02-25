using DotnetSkeleton.SharedKernel.Utils.Models;
using DotnetSkeleton.UserModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.UserModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.UserModule.Domain.Model.Dtos.Users;
using DotnetSkeleton.UserModule.Infrastructure.DbContexts;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace DotnetSkeleton.UserModule.Infrastructure.Repositories.MySQL
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

        public async Task<UserInfo?> FindUserWithRoleByIdAsync(int userId)
        {
            var users = _context.Users;
            var roles = _context.Roles;
            return await (from u in users
                          join r in roles on u.RoleId equals r.RoleId
                          where u.UserId == userId
                          select new UserInfo()
                          {
                              User = u,
                              Role = r
                          }).FirstOrDefaultAsync();
        }

        public async Task<List<UserPaginationDto>> GetAllUsersWithPaginationAsync(int pageNumber, int pageSize, string filter, string sort)
        {
            var queryStatement = @"CALL GetAllUsersWithPagination(@OffsetArg, @LimitArg, @FilterArg, @SortArg)";

            var parameters = new DbParameter[4];
            parameters[2] = new MySqlParameter("@OffsetArg", (pageNumber - 1) * pageSize + 1);
            parameters[1] = new MySqlParameter("@LimitArg", pageNumber * pageSize);
            parameters[3] = new MySqlParameter("@FilterArg", filter);
            parameters[0] = new MySqlParameter("@SortArg", sort);

            var result = _context.Database.SqlQueryRaw<UserPaginationDto>(queryStatement, parameters);

            return await result.ToListAsync();
        }

        /// <summary>
        /// Retrieves user information by email.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns>The task result contains an instance of <see cref="UserInfo"/> if found, otherwise null.</returns>
        public async Task<UserInfo?> FindUserInfoByIdAsync(int userId)
        {
            var users = _context.Users;
            var roles = _context.Roles;
            var userAccounts = _context.UserAccounts;

            var result = (await (from u in users
                    join r in roles on u.RoleId equals r.RoleId into userRoles
                    from ur in userRoles.DefaultIfEmpty()
                    join ua in userAccounts on u.UserId equals ua.UserId into userAccountsGroup
                    from uag in userAccountsGroup.DefaultIfEmpty()
                    where u.UserId == userId && u.IsDeleted == false
                    select new
                    {
                        u,
                        ur,
                        uag
                    }).ToListAsync())
                .GroupBy(g => new
                {
                    g.u,
                    g.ur
                }).Select(g => new UserInfo()
                {
                    User = g.Key.u,
                    Role = g.Key.ur,
                    UserAccount = g.Select(x => x.uag).Where(a => a != null).ToList()
                }).FirstOrDefault();

            return result;
        }
    }
}
