using DotnetSkeleton.IdentityModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.IdentityModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.IdentityModule.Domain.Models.Dto;
using DotnetSkeleton.IdentityModule.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace DotnetSkeleton.IdentityModule.Infrastructure.Repositories.MySQL
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

        /// <summary>
        /// Finds a user and related data based on the specified criteria.
        /// </summary>
        /// <param name="criteriaName">The name of the criteria used for searching.</param>
        /// <param name="criteriaValue">The value of the criteria used for searching.</param>
        /// <returns>The task result contains an instance of <see cref="UserAndRelatedData"/> if found, otherwise null.</returns>
        public async Task<UserAndRelatedData?> FindUserAndRelatedDataByCriteria(string criteriaName, string criteriaValue)
        {
            var users = _context.Users;
            var roles = _context.Roles;
            var result = await (from u in users
                                join r in roles on u.RoleId equals r.RoleId into userRoles
                                from ur in userRoles.DefaultIfEmpty()
                                where EF.Property<string>(u, criteriaName) == criteriaValue
                                select new UserAndRelatedData
                                {
                                    UserId = u.UserId,
                                    Username = u.Username,
                                    Password = u.Password,
                                    Email = u.Email,
                                    FirstName = u.FirstName,
                                    LastName = u.LastName,
                                    MobilePhone = u.MobilePhone,
                                    IsDeleted = u.IsDeleted,
                                    Role = ur != null ? new Role
                                    {
                                        RoleId = ur.RoleId,
                                        Name = ur.Name
                                    } : null
                                }).FirstOrDefaultAsync();

            return result;
        }

        /// <summary>
        /// Retrieves user information by email.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns>The task result contains an instance of <see cref="UserInfo"/> if found, otherwise null.</returns>
        public async Task<UserInfo?> GetUserInfoByEmail(string email)
        {
            var users = _context.Users;
            var roles = _context.Roles;
            var userAccounts = _context.UserAccounts;

            var result = (await (from u in users
                                 join r in roles on u.RoleId equals r.RoleId into userRoles
                                 from ur in userRoles.DefaultIfEmpty()
                                 join ua in userAccounts on u.UserId equals ua.UserId into userAccountsGroup
                                 from uag in userAccountsGroup.DefaultIfEmpty()
                                 where u.Email == email && u.IsDeleted == false
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