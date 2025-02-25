namespace DotnetSkeleton.SharedKernel.Utils.Models.Options
{
    /// <summary>
    /// Represents the options for SignalR.
    /// </summary>
    public class SignalROptions
    {
        public static string JsonKey => nameof(SignalROptions);
        public required Channels Channels { get; set; }
    }

    public class Channels
    {
        public required string NotificationChannel { get; set; }
    }
}