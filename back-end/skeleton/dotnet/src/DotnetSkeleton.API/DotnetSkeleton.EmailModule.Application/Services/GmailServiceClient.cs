using DotnetSkeleton.EmailModule.Domain.Interfaces.Services;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace DotnetSkeleton.EmailModule.Application.Services;

public class GmailServiceClient : IGmailServiceClient
{
    private readonly ILogger<GmailServiceClient> _logger;
    private readonly GmailService _gmailService;
    private readonly MailOptions _mailOptions;

    public GmailServiceClient(ILogger<GmailServiceClient> logger, IOptionsMonitor<MailOptions> mailOptions)
    {
        _logger = logger;
        _mailOptions = mailOptions.CurrentValue;
        _gmailService = InitializeGmailService();
    }

    public async Task<List<Google.Apis.Gmail.v1.Data.Message>> PollUnreadMailsAsync()
    {
        var request = _gmailService.Users.Messages.List("me");
        request.Q = "is:unread";
        var response = await request.ExecuteAsync();

        if (response.Messages is not null && response.Messages.Count > 0)
        {
            _logger.LogInformation("[GmailServiceClient] Found {0} unread messages.", response.Messages.Count);
            return response.Messages.ToList();
        }

        _logger.LogInformation("[GmailServiceClient] No unread messages found.");
        return new List<Google.Apis.Gmail.v1.Data.Message>();
    }

    public async Task<Google.Apis.Gmail.v1.Data.Message> GetEmailMessageAsync(string messageId)
    {
        return await _gmailService.Users.Messages.Get("me", messageId).ExecuteAsync();
    }

    private GmailService InitializeGmailService()
    {
        string[] scopes = { GmailService.Scope.GmailReadonly };
        var appName = "DWF";

        var clientSecrets = new ClientSecrets()
        {
            ClientId = _mailOptions.MailSupport.GmailOAuthClient.ClientId,
            ClientSecret = _mailOptions.MailSupport.GmailOAuthClient.ClientSecret
        };

        var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            clientSecrets,
            scopes,
            "user",
            CancellationToken.None,
            new FileDataStore("token.json", true)).Result;

        _logger.LogInformation("[GmailServiceClient] Service initialized successfully");

        return new GmailService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = appName
        });
    }
}