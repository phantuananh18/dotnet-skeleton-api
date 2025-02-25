using DotnetSkeleton.EmailModule.Domain.Entities;
using DotnetSkeleton.EmailModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.EmailModule.Infrastructure.DbContexts;

namespace DotnetSkeleton.EmailModule.Infrastructure.Repositories;

public class CommunicationTemplateRepository : BaseRepository<CommunicationTemplate, int>, ICommunicationTemplateRepository
{
    private readonly SkeletonDbContext _context;

    public CommunicationTemplateRepository(SkeletonDbContext context) : base(context)
    {
        _context = context;
    }

    // TODO: Implement methods
}