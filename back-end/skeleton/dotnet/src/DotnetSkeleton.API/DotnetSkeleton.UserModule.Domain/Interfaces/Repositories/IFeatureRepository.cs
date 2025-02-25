using DotnetSkeleton.UserModule.Domain.Entities.MySQLEntities;

namespace DotnetSkeleton.UserModule.Domain.Interfaces.Repositories
{
    public interface IFeatureRepository : IBaseRepository<Feature, int>
    {
    }
}