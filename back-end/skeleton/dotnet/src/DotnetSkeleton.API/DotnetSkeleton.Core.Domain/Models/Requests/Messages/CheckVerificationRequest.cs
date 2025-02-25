namespace DotnetSkeleton.Core.Domain.Models.Requests.Messages
{
    public class CheckVerificationRequest
    {
        public required string MobilePhone { get; set; }
        public required string Code { get; set; }
    }
}