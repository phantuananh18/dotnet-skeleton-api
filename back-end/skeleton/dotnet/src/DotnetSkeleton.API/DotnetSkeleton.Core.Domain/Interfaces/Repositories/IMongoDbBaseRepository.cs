using System.Linq.Expressions;

namespace DotnetSkeleton.Core.Domain.Interfaces.Repositories;

/// <summary>
/// Represents a base repository for MongoDB interface for CRUD operations on entities.
/// See more EF Core for MongoDB: https://www.mongodb.com/developer/how-to/ef-core-mongodb/
/// </summary>
/// <typeparam name="TEntity">The type of the entity a.k.a collections</typeparam>
/// <typeparam name="TKey">The key of the entity a.k.a ObjectId</typeparam>
public interface IMongoDBBaseRepository<TEntity, in TKey> where TEntity : class
{
    /// <summary>
    /// Finds collections as entities that match the specified predicate asynchronously.
    /// </summary>
    /// <param name="expression">The predicate to filter the entities.</param>
    /// <returns>The task returns a result containing an IEnumerable of entities that match the predicate.</returns>
    Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Finds the first collection as entity that matches the specified predicate asynchronously.
    /// </summary>
    /// <param name="expression">The predicate to filter the entities.</param>
    /// <returns>The task returns the first entity that matches the predicate or null if no such entity is found.</returns>
    Task<TEntity?> FindOneByAsync(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Finds an collection as entity with the specified ID asynchronously.
    /// </summary>
    /// <param name="id">The Id/ObjectId of the collection.</param>
    /// <returns>The task result contains the entity with the specified ID, or null if no such entity is found.</returns>
    Task<TEntity?> FindByIdAsync(TKey id);

    /// <summary>
    /// Gets all entities asynchronously.
    /// </summary>
    /// <returns>The task result contains a list of all entities.</returns>
    Task<IEnumerable<TEntity>> FindAllAsync();

    /// <summary>
    /// Adds a new entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the added entity</returns>
    Task<TEntity> AddAsync(TEntity entity);

    /// <summary>
    /// Updates an existing entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>The task result contains the updated entity.</returns>
    Task<TEntity> UpdateAsync(TEntity entity);

    /// <summary>
    /// Deletes an entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <returns>The task result contains a boolean value indicating whether the entity was successfully deleted.</returns>
    Task<bool> DeleteAsync(TEntity entity);

    /// <summary>
    /// Gets the count of all entities asynchronously.
    /// </summary>
    /// <returns>The task result contains the count of all entities.</returns>
    Task<int> CountAsync();

    /// <summary>
    /// Gets the count of entities that match the specified predicate asynchronously.
    /// </summary>
    /// <param name="expression">The predicate to filter entities.</param>
    /// <returns>The task result contains the count of entities that match the predicate.</returns>
    Task<int> CountByAsync(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Inserts a list of entities into the database asynchronously.
    /// </summary>
    /// <param name="entities">The list of entities to insert.</param>
    /// <returns>The task result contains the inserted entities.</returns>
    Task<List<TEntity>> BulkInsertAsync(List<TEntity> entities);

    /// <summary>
    /// Updates a list of entities in the database asynchronously.
    /// </summary>
    /// <param name="entities">The list of entities to update.</param>
    /// <returns>The task result contains a boolean value indicating whether the entities were successfully updated.</returns>
    Task<bool> BulkUpdateAsync(List<TEntity> entities);

    /// <summary>
    /// Deletes a list of entities from the database asynchronously.
    /// </summary>
    /// <param name="entities">The list of entities to delete.</param>
    /// <returns>The task result contains a boolean value indicating whether the entities were successfully deleted.</returns>
    Task<bool> BulkDeleteAsync(List<TEntity> entities);

    /// <summary>
    /// Rolls back any pending changes made to the entities in the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task Rollback();
}