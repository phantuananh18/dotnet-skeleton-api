namespace DotnetSkeleton.SharedKernel.Utils.Models.Options
{
    public class SystemInfoOptions
    {
        public static string JsonKey => nameof(SystemInfoOptions);
        public required string AppName { get; set; }
        public required string SystemEmail { get; set; }
        public string? ForgotPasswordCallbackUrl { get; set; }
        public string? EmailServiceUrl { get; set; }
        public string? IdentityServiceUrl { get; set; }
        public string? MessageServiceUrl { get; set; }
        public string? NotificationServiceUrl { get; set; }
        public string? QueueServiceUrl { get; set; }
        public string? UserServiceUrl { get; set; }
    }
}