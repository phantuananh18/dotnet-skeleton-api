using DotnetSkeleton.Core.Domain.Interfaces.Repositories;
using DotnetSkeleton.Core.Infrastructure.DbContexts;
using System.Collections;

namespace DotnetSkeleton.Core.Infrastructure.Repositories.MySQL;

public class UnitOfWork
{
    private readonly SkeletonDbContext _context;
    private Hashtable _repositories;
    private bool _disposed;


    public UnitOfWork(SkeletonDbContext context, Hashtable repositories, bool disposed)
    {
        _context = context;
        _repositories = repositories;
        _disposed = disposed;
    }

    public IBaseRepository<TEntity, TKey> CreateRepository<TEntity, TKey>() where TEntity : class
    {
        _repositories ??= new Hashtable();
        var type = typeof(TEntity).Name;
        if (_repositories.ContainsKey(type))
        {
            return (IBaseRepository<TEntity, TKey>)_repositories[type]!;
        }

        var repositoryType = typeof(BaseRepository<TEntity, TKey>);
        var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
        _repositories.Add(type, repositoryInstance);

        return (IBaseRepository<TEntity, TKey>)_repositories[type]!;
    }


}