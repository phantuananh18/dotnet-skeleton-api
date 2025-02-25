using DotnetSkeleton.UserModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.UserModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.UserModule.Domain.Model.Dtos.Permission;
using DotnetSkeleton.UserModule.Infrastructure.DbContexts;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace DotnetSkeleton.UserModule.Infrastructure.Repositories.MySQL
{
    public class PermissionRepository : BaseRepository<Permission, int>, IPermissionRepository
    {
        private readonly SkeletonDbContext _context;

        public PermissionRepository(SkeletonDbContext context) : base(context)
        {
            _context = context;
        }

        // TO-DO: Implement custom methods for RoleRepository
        public async Task<List<PermissionPaginationDto>> GetAllPermissionWithPaginationAsync(string roleFilter, string sort)
        {
            var queryStatement = @"CALL GetAllFeaturesWithPermissions(@RoleArg, @SortArg)";

            var parameters = new DbParameter[2];
            parameters[0] = new MySqlParameter("@RoleArg", roleFilter);
            parameters[1] = new MySqlParameter("@SortArg", sort);

            var result = _context.Database.SqlQueryRaw<PermissionPaginationDto>(queryStatement, parameters).ToListAsync();

            return await result;
        }
    }
}