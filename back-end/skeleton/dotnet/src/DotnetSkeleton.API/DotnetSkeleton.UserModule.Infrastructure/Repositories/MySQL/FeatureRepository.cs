using DotnetSkeleton.UserModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.UserModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.UserModule.Infrastructure.DbContexts;

namespace DotnetSkeleton.UserModule.Infrastructure.Repositories.MySQL
{
    public class FeatureRepository(SkeletonDbContext context) : BaseRepository<Feature, int>(context), IFeatureRepository
    {
    }
}