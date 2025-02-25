using System.Linq.Expressions;

namespace DotnetSkeleton.UserModule.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Represents a base repository interface for CRUD operations on entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IBaseRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// Finds entities that match the specified predicate asynchronously.
        /// </summary>
        /// <param name="predicate">The predicate used to filter the entities.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of entities that match the predicate.</returns>
        Task<List<TEntity?>> FindByAsync(Expression<Func<TEntity?, bool>> predicate);

        /// <summary>
        /// Finds the first entity that matches the specified predicate asynchronously.
        /// </summary>
        /// <param name="predicate">The predicate used to filter the entities.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the first entity that matches the predicate, or null if not found.</returns>
        Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Finds an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity to find.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the found entity, or null if not found.</returns>
        Task<TEntity?> FindByIdAsync(TKey id);

        /// <summary>
        /// Gets all entities asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of all entities.</returns>
        Task<List<TEntity>> FindAllAsync();

        /// <summary>
        /// Adds a new entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added entity.</returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Updates an existing entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated entity.</returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Deletes an entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the entity was deleted successfully.</returns>
        Task<bool> DeleteAsync(TEntity entity);

        /// <summary>
        /// Counts the total number of entities asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the total number of entities.</returns>
        Task<int> CountAsync();

        /// <summary>
        /// Counts the number of entities that match the specified predicate asynchronously.
        /// </summary>
        /// <param name="predicate">The predicate used to filter the entities.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the number of entities that match the predicate.</returns>
        Task<int> CountByAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Inserts a list of entities asynchronously.
        /// </summary>
        /// <param name="entities">The list of entities to insert.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the entities were inserted successfully.</returns>
        Task<List<TEntity>> BulkInsertAsync(List<TEntity> entities);

        /// <summary>
        /// Updates a list of entities asynchronously.
        /// </summary>
        /// <param name="entities">The list of entities to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the entities were updated successfully.</returns>
        Task<bool> BulkUpdateAsync(List<TEntity> entities);

        /// <summary>
        /// Deletes a list of entities asynchronously.
        /// </summary>
        /// <param name="entities">The list of entities to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the entities were deleted successfully.</returns>
        Task<bool> BulkDeleteAsync(List<TEntity> entities);

        /// <summary>
        /// Rolls back any pending changes made to the entities in the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task Rollback();
    }
}
