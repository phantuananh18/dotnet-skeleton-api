namespace DotnetSkeleton.SharedKernel.Utils.Models.Options
{
    public class TwilioOptions
    {
        public static string JsonKey => nameof(TwilioOptions);
        public required string AccountSID { get; set; }
        public required string AuthToken { get; set; }
        public required string VerificationSid { get; set; }
        public required string FromPhoneNumber { get; set; }
    }
}