namespace DotnetSkeleton.SharedKernel.Utils.Models.Options;

public class RedisOptions
{
    /// <summary>
    /// Gets the JSON key for the <see cref="RedisOptions"/> class.
    /// </summary>
    public static string JsonKey => nameof(RedisOptions);

    /// <summary>
    /// Gets or sets the connection string of Redis cache.
    /// </summary>
    public required string ConnectionString { get; set; }

    /// <summary>
    /// Gets or sets the Redis cache instance name.
    /// </summary>
    public required string InstanceName { get; set; }

    /// <summary>
    /// Gets or sets the default AbsoluteExpirationRelativeToNow when setting cache.
    /// </summary>
    public TimeSpan AbsoluteExpirationRelativeToNow { get; set; } = TimeSpan.Zero;
}