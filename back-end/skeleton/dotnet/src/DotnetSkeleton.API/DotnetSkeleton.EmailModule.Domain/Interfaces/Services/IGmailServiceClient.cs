namespace DotnetSkeleton.EmailModule.Domain.Interfaces.Services
{
    public interface IGmailServiceClient
    {
        Task<List<Google.Apis.Gmail.v1.Data.Message>> PollUnreadMailsAsync();
        Task<Google.Apis.Gmail.v1.Data.Message> GetEmailMessageAsync(string messageId);
    }
}
