namespace DotnetSkeleton.Core.Domain.Models.Requests.Messages
{
    public class SendSmsRequest
    {
        public required string ToMobilePhone { get; set; }
        public required string Message { get; set; }
    }
}