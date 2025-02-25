namespace DotnetSkeleton.SharedKernel.Utils.Models.Options
{
    public class MailOptions
    {
        public static string JsonKey => nameof(MailOptions);
        public required NoReply NoReply { get; set; }
        public required MailSupport MailSupport { get; set; }
    }


    public class NoReply
    {
        public required string Mail { get; set; }
        public required string Password { get; set; }
        public required string Host { get; set; }
        public int Port { get; set; }
    }

    public class MailSupport
    {
        public required string Mail { get; set; }
        public required string Password { get; set; }
        public required string Host { get; set; }
        public int Port { get; set; }
        public required GmailOAuthClient GmailOAuthClient { get; set; }
    }

    public class GmailOAuthClient
    {
        public required string ClientId { get; set; }
        public required string ClientSecret { get; set; }
        public required string ProjectId { get; set; }
        public required string AuthUri { get; set; }
        public required string TokenUri { get; set; }
        public required string AuthProviderCert { get; set; }
    }
}