namespace DotnetSkeleton.NotificationModule.Domain.Model.Requests.MessageInstance
{
    public class SendNotificationRequest
    {
        public int? TriggeredUserId { get; set; }
        public required string NotificationType { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string SenderInfo { get; set; }
    }
}