using DotnetSkeleton.EmailModule.Domain.Entities;

namespace DotnetSkeleton.EmailModule.Domain.Interfaces.Repositories;

/// <summary>
/// Represents a repository for managing user entities.
/// </summary>
public interface IUserRepository : IBaseRepository<User, int>
{

}