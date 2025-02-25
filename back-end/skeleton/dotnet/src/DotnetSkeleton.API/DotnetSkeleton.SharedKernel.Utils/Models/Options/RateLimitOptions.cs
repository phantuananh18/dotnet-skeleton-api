using System.Threading.RateLimiting;

namespace DotnetSkeleton.SharedKernel.Utils.Models.Options;

public class RateLimitOptions
{
    /// <summary>
    /// Gets the JSON key for the <see cref="RateLimitOptions"/> class.
    /// </summary>
    public static string JsonKey => nameof(RateLimitOptions);

    public required TokenBucketRateLimiterOptions TokenBucketRateLimiter { get; set; }
}