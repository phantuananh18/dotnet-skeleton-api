using DotnetSkeleton.UserModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.UserModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.UserModule.Domain.Model.Dtos.Roles;
using DotnetSkeleton.UserModule.Infrastructure.DbContexts;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace DotnetSkeleton.UserModule.Infrastructure.Repositories.MySQL
{
    /// <summary>
    /// Role repository
    /// </summary>
    public class RoleRepository : BaseRepository<Role, int>, IRoleRepository
    {
        private readonly SkeletonDbContext _skeletonDbContext;

        public RoleRepository(SkeletonDbContext skeletonDbContext) : base(skeletonDbContext)
        {
            _skeletonDbContext = skeletonDbContext;
        }

        public async Task<List<RolePaginationDto>> GetAllRolesWithPaginationAsync(int pageNumber, int pageSize, string searchText, string sort)
        {
            var queryStatement = $@"CALL GetAllRolesWithPagination(@OffsetArg, @LimitArg, @SearchTextArg, @SortArg)";

            var parameters = new DbParameter[4];
            parameters[0] = new MySqlParameter("@SortArg", sort);
            parameters[1] = new MySqlParameter("@LimitArg", pageNumber * pageSize);
            parameters[2] = new MySqlParameter("@OffsetArg", (pageNumber - 1) * pageSize + 1);
            parameters[3] = new MySqlParameter("@SearchTextArg", searchText);

            var result = _skeletonDbContext.Database.SqlQueryRaw<RolePaginationDto>(queryStatement, parameters);

            return await result.ToListAsync();
        }
    }
}
