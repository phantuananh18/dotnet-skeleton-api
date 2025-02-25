﻿using DotnetSkeleton.SharedKernel.Utils.Configures.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DotnetSkeleton.SharedKernel.Utils.Configures.Databases.MySQL;

/// <summary>
/// Represents a base repository implementation that provides common CRUD operations for entities for MySQL with Entity Framework Core.
/// </summary>
/// <typeparam name="TDbContext">The type of DbContext</typeparam>
/// <typeparam name="TEntity">The type of the entity</typeparam>
/// <typeparam name="TKey">The type of the primary key</typeparam>
public class BaseRepository<TDbContext, TEntity, TKey> : IDisposable, IBaseRepository<TDbContext, TEntity, TKey>
    where TDbContext : DbContext
    where TEntity : class
{
    private readonly TDbContext _context;
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseRepository{TDbContext, TEntity, TKey}"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public BaseRepository(TDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Releases all resources used by the <see cref="BaseRepository{TDbContext, TEntity, TKey}"/> object.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="BaseRepository{TDbContext, TEntity, TKey}"/> object and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
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
    /// Finds entities that match the specified predicate asynchronously.
    /// </summary>
    /// <param name="predicate">The predicate to filter entities.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of entities that match the predicate.</returns>
    public async Task<List<TEntity?>> FindByAsync(Expression<Func<TEntity?, bool>> predicate)
    {
        return await _context.Set<TEntity>().Where(predicate).ToListAsync();
    }

    /// <summary>
    /// Finds the first entity that matches the specified predicate asynchronously.
    /// </summary>
    /// <param name="predicate">The predicate to filter entities.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the first entity that matches the predicate, or null if no such entity is found.</returns>
    public async Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }

    /// <summary>
    /// Finds an entity with the specified ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the entity.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity with the specified ID, or null if no such entity is found.</returns>
    public async Task<TEntity?> FindByIdAsync(TKey id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    /// <summary>
    /// Gets all entities asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of all entities.</returns>
    public async Task<List<TEntity>> FindAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    /// <summary>
    /// Gets all entities with pagination and sorting asynchronously.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The number of entities per page.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of entities.</returns>
    public async Task<List<TEntity>> FindAllWithPaginationAsync(int pageNumber, int pageSize)
    {
        return await _context.Set<TEntity>().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    /// <summary>
    /// Adds a new entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the added entity.</returns>
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Updates an existing entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated entity.</returns>
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
        await _context.Entry(entity).ReloadAsync();

        return entity;
    }

    /// <summary>
    /// Deletes an entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the entity was successfully deleted.</returns>
    public async Task<bool> DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Gets the count of all entities asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the count of all entities.</returns>
    public async Task<int> CountAsync()
    {
        return await _context.Set<TEntity>().CountAsync();
    }

    /// <summary>
    /// Gets the count of entities that match the specified predicate asynchronously.
    /// </summary>
    /// <param name="predicate">The predicate to filter entities.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the count of entities that match the predicate.</returns>
    public async Task<int> CountByAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().CountAsync(predicate);
    }

    /// <summary>
    /// Inserts a list of entities into the database asynchronously.
    /// </summary>
    /// <param name="entities">The list of entities to insert.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the inserted entities.</returns>
    public async Task<List<TEntity>> BulkInsertAsync(List<TEntity> entities)
    {
        _context.ChangeTracker.AutoDetectChangesEnabled = false;
        for (int i = 0; i < entities.Count; i += 100)
        {
            var batch = entities.Skip(i).Take(50).ToList();
            await _context.Set<TEntity>().AddRangeAsync(batch);
            await _context.SaveChangesAsync();
            await Task.Delay(500);
        }

        return entities;
    }

    /// <summary>
    /// Updates a list of entities in the database asynchronously.
    /// </summary>
    /// <param name="entities">The list of entities to update.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the entities were successfully updated.</returns>
    public async Task<bool> BulkUpdateAsync(List<TEntity> entities)
    {
        _context.ChangeTracker.AutoDetectChangesEnabled = false;
        _context.Set<TEntity>().UpdateRange(entities);
        return await _context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Deletes a list of entities from the database asynchronously.
    /// </summary>
    /// <param name="entities">The list of entities to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the entities were successfully deleted.</returns>
    public async Task<bool> BulkDeleteAsync(List<TEntity> entities)
    {
        _context.ChangeTracker.AutoDetectChangesEnabled = false;
        _context.Set<TEntity>().RemoveRange(entities);
        return await _context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Rolls back any pending changes made to the entities in the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task Rollback()
    {
        _context.ChangeTracker.Entries().ToList().ForEach(e => e.ReloadAsync());
        return Task.CompletedTask;
    }
}