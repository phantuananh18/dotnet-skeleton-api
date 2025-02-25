namespace DotnetSkeleton.SharedKernel.Utils.Models.Options
{
    public class HttpClientRetryOptions
    {
        public static string JsonKey => nameof(HttpClientRetryOptions);
        public int MaxRetryAttempts { get; set; }
        public int Delay { get; set; }
        public int Timeout { get; set; }
    }
}