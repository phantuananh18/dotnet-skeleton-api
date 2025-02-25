namespace DotnetSkeleton.NotificationModule.Domain.Model.Requests.NewFolder
{
    public class SendNotificationContent
    {
        public int? TriggeredUserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public required string NotificationType { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string SenderInfo { get; set; }
    }
}