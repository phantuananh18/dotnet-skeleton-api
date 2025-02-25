using DotnetSkeleton.Core.Domain.Interfaces.Repositories;
using DotnetSkeleton.Core.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DotnetSkeleton.Core.Infrastructure.Repositories.MongoDb;

/// <summary>
/// Represents a base repository implementation that provides common CRUD operations for entities for MongoDB with Entity Framework Core.
/// See more EF Core for MongoDB: https://www.mongodb.com/developer/how-to/ef-core-mongodb/
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TKey">The type of the primary key. (Usually string a.k.a ObjectId)</typeparam>
public class MongoDBBaseRepository<TEntity, TKey> : IMongoDBBaseRepository<TEntity, TKey>, IDisposable where TEntity : class
{
    private readonly SkeletonMongoDbContext _context;
    private readonly DbSet<TEntity> _dbSet;
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="MongoDBBaseRepository{TEntity, TKey}"/> class.
    /// </summary>
    /// <param name="context">The database context</param>
    public MongoDBBaseRepository(SkeletonMongoDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<TEntity>();
    }

    /// <summary>
    /// Releases all resources used by the <see cref="MongoDBBaseRepository{TEntity, TKey}"/> object.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="MongoDBBaseRepository{TEntity, TKey}"/> object and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">True to release both managed and unmanaged resources; False to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        _disposed = true;
    }

    /// <summary>
    /// Finds collections as entities that match the specified predicate asynchronously.
    /// </summary>
    /// <param name="expression">The predicate to filter the entities.</param>
    /// <returns>The task returns a result containing an IEnumerable of entities that match the predicate.</returns>
    public async Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbSet.AsQueryable().Where(expression).ToListAsync();
    }

    /// <summary>
    /// Finds the first collection as entity that matches the specified predicate asynchronously.
    /// </summary>
    /// <param name="expression">The predicate to filter the entities.</param>
    /// <returns>The task returns the first entity that matches the predicate or null if no such entity is found.</returns>
    public async Task<TEntity?> FindOneByAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbSet.AsQueryable().FirstOrDefaultAsync(expression);
    }

    /// <summary>
    /// Finds an collection as entity with the specified ID asynchronously.
    /// </summary>
    /// <param name="id">The Id/ObjectId of the collection.</param>
    /// <returns>The task result contains the entity with the specified ID, or null if no such entity is found.</returns>
    public async Task<TEntity?> FindByIdAsync(TKey id)
    {
        return await _dbSet.FindAsync(id);
    }

    /// <summary>
    /// Gets all entities asynchronously.
    /// </summary>
    /// <returns>The task result contains a list of all entities.</returns>
    public async Task<IEnumerable<TEntity>> FindAllAsync()
    {
        return await _dbSet.AsQueryable().ToListAsync();
    }

    /// <summary>
    /// Adds a new entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the added entity</returns>
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Updates an existing entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>The task result contains the updated entity.</returns>
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Deletes an entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <returns>The task result contains a boolean value indicating whether the entity was successfully deleted.</returns>
    public async Task<bool> DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Gets the count of all entities asynchronously.
    /// </summary>
    /// <returns>The task result contains the count of all entities.</returns>
    public async Task<int> CountAsync()
    {
        return await _dbSet.AsQueryable().CountAsync();
    }

    /// <summary>
    /// Gets the count of entities that match the specified predicate asynchronously.
    /// </summary>
    /// <param name="expression">The predicate to filter entities.</param>
    /// <returns>The task result contains the count of entities that match the predicate.</returns>
    public async Task<int> CountByAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbSet.AsQueryable().CountAsync(expression);
    }

    /// <summary>
    /// Inserts a list of entities into the database asynchronously.
    /// </summary>
    /// <param name="entities">The list of entities to insert.</param>
    /// <returns>The task result contains the inserted entities.</returns>
    public async Task<List<TEntity>> BulkInsertAsync(List<TEntity> entities)
    {
        _context.ChangeTracker.AutoDetectChangesEnabled = false;
        await _dbSet.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
        return entities;
    }

    /// <summary>
    /// Updates a list of entities in the database asynchronously.
    /// </summary>
    /// <param name="entities">The list of entities to update.</param>
    /// <returns>The task result contains a boolean value indicating whether the entities were successfully updated.</returns>
    public async Task<bool> BulkUpdateAsync(List<TEntity> entities)
    {
        _context.ChangeTracker.AutoDetectChangesEnabled = false;
        _dbSet.UpdateRange(entities);
        return await _context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Deletes a list of entities from the database asynchronously.
    /// </summary>
    /// <param name="entities">The list of entities to delete.</param>
    /// <returns>The task result contains a boolean value indicating whether the entities were successfully deleted.</returns>
    public async Task<bool> BulkDeleteAsync(List<TEntity> entities)
    {
        _context.ChangeTracker.AutoDetectChangesEnabled = false;
        _dbSet.RemoveRange(entities);
        return await _context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Rolls back any pending changes made to the entities in the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task Rollback()
    {
        _context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        return Task.CompletedTask;
    }
}