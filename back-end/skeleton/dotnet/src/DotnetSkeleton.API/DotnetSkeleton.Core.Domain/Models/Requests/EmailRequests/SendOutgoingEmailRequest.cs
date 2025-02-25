using static DotnetSkeleton.SharedKernel.Utils.Constant;

namespace DotnetSkeleton.Core.Domain.Models.Requests.EmailRequests;

/// <summary>
/// The request model for sending an email.
/// </summary>
public class OutgoingEmailRequest
{
    /// <summary>
    /// The email type.
    /// </summary>
    public EmailType EmailType { get; set; }

    /// <summary>
    /// The email address of the sender.
    /// </summary>
    public string? Sender { get; set; }

    /// <summary>
    /// The email addresses of the recipients.
    /// </summary>
    public required List<string> To { get; set; }

    /// <summary>
    /// The email addresses of the CC recipients.
    /// </summary>
    public List<string>? Cc { get; set; } = new List<string>();

    /// <summary>
    /// The email addresses of the BCC recipients.
    /// </summary>
    public List<string>? Bcc { get; set; } = new List<string>();

    /// <summary>
    /// The subject of the email.
    /// </summary>
    public string? Subject { get; set; }

    /// <summary>
    /// The body of the email.
    /// Optional, the email's body can be built based on the template and placeholders.
    /// Priority is given to the body if it is provided.
    /// </summary>
    public string? Body { get; set; }

    /// <summary>
    /// The name of the template to use for the email.
    /// </summary>
    public string? TemplateName { get; set; }

    /// <summary>
    /// The placeholders to be replaced in the email body (optional).
    /// </summary>
    public Dictionary<string, string>? TemplatePlaceHolders { get; set; }
}