namespace DotnetSkeleton.Core.Domain.Models.Requests.Messages
{
    public class StartVerificationRequest
    {
        public required string MobilePhone { get; set; }
        public required string Chanel { get; set; }
    }
}