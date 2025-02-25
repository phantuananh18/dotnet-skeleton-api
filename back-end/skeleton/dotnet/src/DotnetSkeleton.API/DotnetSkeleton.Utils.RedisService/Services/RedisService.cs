using System.Text;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using DotnetSkeleton.Utils.RedisService.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DotnetSkeleton.Utils.RedisService.Services;

public class RedisService : IRedisService
{
    #region Private Members
    private readonly ILogger<RedisService> _logger;
    private readonly IDistributedCache _cache;
    private readonly RedisOptions _cacheOptions;

    #endregion

    #region Constructor
    public RedisService(ILogger<RedisService> logger, IDistributedCache redis,
        IOptionsMonitor<RedisOptions> redisCacheOptions)
    {
        _logger = logger;
        _cache = redis;
        _cacheOptions = redisCacheOptions.CurrentValue;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Gets a value with the given key.
    /// </summary>
    /// <param name="key">A string identifying the requested value.</param>
    /// <param name="cancellationToken">
    /// Optional. The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.
    /// </param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the located value or null.</returns>
    public async Task<string?> GetAsync(string key, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("[RedisService/GetAsync] Start request to get cache with key: {key}", key);
            var result = await _cache.GetAsync(key, cancellationToken);
            return result == null ? null : Decode(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("[RedisService/GetAsync] Failed request to get cache with exception: {ex}", ex);
            return null;
        }
    }

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
    public async Task<bool> SetAsync(string key, string? value, DistributedCacheEntryOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value), @"Key cannot be null or empty.");
        }

        try
        {
            // Default DistributedCacheEntryOptions will only include AbsoluteExpirationRelativeToNow
            options ??= new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = _cacheOptions.AbsoluteExpirationRelativeToNow
            };

            await _cache.SetAsync(key, Encode(value), options, cancellationToken);
            _logger.LogInformation("[RedisService/SetAsync] Added new cache with key: {key}", key);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("[RedisService/SetAsync] Failed to set cache for key: {key}. Exception: {ex}", key, ex);
            return false;
        }
    }

    /// <summary>
    /// Refreshes a value in the cache based on its key, resetting its sliding expiration timeout (if any).
    /// </summary>
    /// <param name="key">A string identifying the requested value.</param>
    /// <param name="cancellationToken">
    /// Optional. The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.
    /// </param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    public async Task RefreshAsync(string key, CancellationToken cancellationToken = default)
    {
        await _cache.RefreshAsync(key, cancellationToken);
        _logger.LogInformation("[RedisService/RefreshAsync] Refreshed cache with key: {key}", key);
    }

    /// <summary>
    /// Removes the value with the given key.
    /// </summary>
    /// <param name="key">A string identifying the requested value.</param>
    /// <param name="cancellationToken">
    /// Optional. The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.
    /// </param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await _cache.RemoveAsync(key, cancellationToken);
        _logger.LogInformation("[RedisService/RemoveAsync] Removed cache with key: {key}", key);
    }

    /// <summary>
    /// Retrieves a value from the cache for the specified key. 
    /// If the value does not exist, sets the provided value in the cache and returns it.
    /// </summary>
    /// <param name="key">A string identifying the requested value.</param>
    /// <param name="value">The value to set in the cache if the key does not exist.</param>
    /// <param name="options">Optional cache entry options for the value being set.</param>
    /// <param name="cancellationToken">Optional token to propagate cancellation notifications.</param>
    /// <returns>The value from the cache if it exists; otherwise, the newly set value.</returns>
    public async Task<string?> GetAndSetAsync(string key, string? value, DistributedCacheEntryOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        var result = await GetAsync(key, cancellationToken);
        if (!string.IsNullOrEmpty(result))
        {
            return result;
        }
        
        // If not exist, try add new key with this value to cache
        await SetAsync(key, value, options, cancellationToken);
        return value;
    }

    /// <summary>
    /// Retrieves a value from the cache for the specified key. 
    /// If the value exists, refreshes its sliding expiration timeout and returns it.
    /// </summary>
    /// <param name="key">A string identifying the requested value.</param>
    /// <param name="cancellationToken">Optional token to propagate cancellation notifications.</param>
    /// <returns>The value from the cache if it exists; otherwise, null.</returns>
    public async Task<string?> GetAndRefreshAsync(string key, CancellationToken cancellationToken = default)
    {
        var result = await GetAsync(key, cancellationToken);
        if (string.IsNullOrEmpty(result))
        {
            return null;
        }

        await RefreshAsync(key, cancellationToken);
        return result;
    }

    #endregion

    #region Private Methods
    private static byte[] Encode(string value) => Encoding.UTF8.GetBytes(value);

    private static string Decode(byte[] value) => Encoding.UTF8.GetString(value);

    #endregion
}