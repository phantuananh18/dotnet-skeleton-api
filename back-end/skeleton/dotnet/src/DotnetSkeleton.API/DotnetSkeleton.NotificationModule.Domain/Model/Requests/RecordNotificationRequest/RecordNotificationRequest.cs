namespace DotnetSkeleton.NotificationModule.Domain.Model.Requests.RecordNotification
{
    public class RecordNotificationRequest
    {
        public int? TriggeredUserId { get; set; }
        public long NotificationTypeId { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string SenderInfo { get; set; }
        public int CreatedBy { get; set; }
        public string? FilterArg { get; set; }
    }
}