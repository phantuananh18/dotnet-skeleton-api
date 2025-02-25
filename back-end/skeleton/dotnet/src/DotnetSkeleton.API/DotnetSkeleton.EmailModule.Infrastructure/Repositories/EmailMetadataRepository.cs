using DotnetSkeleton.EmailModule.Domain.Entities;
using DotnetSkeleton.EmailModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.EmailModule.Infrastructure.DbContexts;

namespace DotnetSkeleton.EmailModule.Infrastructure.Repositories;

public class EmailMetadataRepository : BaseRepository<EmailMetadata, int>, IEmailMetadataRepository
{
    private readonly SkeletonDbContext _context;

    public EmailMetadataRepository(SkeletonDbContext context) : base(context)
    {
        _context = context;
    }

    // TODO: Implement methods
}