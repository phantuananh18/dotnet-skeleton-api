using DotnetSkeleton.EmailModule.Domain.Entities;
using DotnetSkeleton.EmailModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.EmailModule.Infrastructure.DbContexts;

namespace DotnetSkeleton.EmailModule.Infrastructure.Repositories;

public class CommunicationRepository : BaseRepository<Communication, int>, ICommunicationRepository
{
    private readonly SkeletonDbContext _context;

    public CommunicationRepository(SkeletonDbContext context) : base(context)
    {
        _context = context;
    }

    // TODO: Implement methods
}