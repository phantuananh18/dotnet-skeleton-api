using DotnetSkeleton.EmailModule.Domain.Entities;
using DotnetSkeleton.EmailModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.EmailModule.Infrastructure.DbContexts;

namespace DotnetSkeleton.EmailModule.Infrastructure.Repositories;

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

}