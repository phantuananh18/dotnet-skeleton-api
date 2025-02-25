using Microsoft.Extensions.Caching.Distributed;

namespace DotnetSkeleton.Utils.RedisService.Interfaces;

public interface IRedisService
{
    /// <summary>
    /// Gets a value with the given key.
    /// </summary>
    /// <param name="key">A string identifying the requested value.</param>
    /// <param name="cancellationToken">
    /// Optional. The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.
    /// </param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the located value or null.</returns>
    Task<string?> GetAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the value with the given key.
    /// </summary>
    /// <param name="key">A string identifying the requested value.</param>
    /// <param name="value">The value to set in the cache.</param>
    /// <param name="options">The cache options for the value.</param>
    /// <param name="cancellationToken">
    /// Optional. The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.
    /// </param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task<bool> SetAsync(string key, string? value, DistributedCacheEntryOptions? options = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Refreshes a value in the cache based on its key, resetting its sliding expiration timeout (if any).
    /// </summary>
    /// <param name="key">A string identifying the requested value.</param>
    /// <param name="cancellationToken">
    /// Optional. The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.
    /// </param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task RefreshAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes the value with the given key.
    /// </summary>
    /// <param name="key">A string identifying the requested value.</param>
    /// <param name="cancellationToken">
    /// Optional. The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.
    /// </param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a value from the cache for the specified key. 
    /// If the value does not exist, sets the provided value in the cache and returns it.
    /// </summary>
    /// <param name="key">A string identifying the requested value.</param>
    /// <param name="value">The value to set in the cache if the key does not exist.</param>
    /// <param name="options">Optional cache entry options for the value being set.</param>
    /// <param name="cancellationToken">Optional token to propagate cancellation notifications.</param>
    /// <returns>The value from the cache if it exists; otherwise, the newly set value.</returns>
    Task<string?> GetAndSetAsync(string key, string? value, DistributedCacheEntryOptions? options = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a value from the cache for the specified key. 
    /// If the value exists, refreshes its sliding expiration timeout and returns it.
    /// </summary>
    /// <param name="key">A string identifying the requested value.</param>
    /// <param name="cancellationToken">Optional token to propagate cancellation notifications.</param>
    /// <returns>The value from the cache if it exists; otherwise, null.</returns>
    Task<string?> GetAndRefreshAsync(string key, CancellationToken cancellationToken = default);
}