namespace DotnetSkeleton.SharedKernel.Utils.Models.Requests;

/// <summary>
/// Represents the parameters required for sending an HTTP request.
/// </summary>
public class BaseHttpRequestParameter
{
    /// <summary>
    /// Gets or sets the HTTP method (e.g., GET, POST, PUT, DELETE) for the request.
    /// </summary>
    public required HttpMethod Method { get; set; }

    /// <summary>
    /// Gets or sets the base URL for the HTTP request.
    /// </summary>
    public string BaseUrl { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the specific endpoint to append to the base URL.
    /// </summary>
    public string Endpoint { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the query parameters to be included in the request URL.
    /// </summary>
    public Dictionary<string, string>? QueryParams { get; set; }

    /// <summary>
    /// Gets or sets the headers to be included in the HTTP request.
    /// </summary>
    public Dictionary<string, string>? Headers { get; set; } = new Dictionary<string, string>()
    {
        { "Accept", "*/*" }
    };

    /// <summary>
    /// Gets or sets the files to be uploaded with the request.
    /// </summary>
    public Dictionary<string, Stream>? Files { get; set; }

    /// <summary>
    /// Gets or sets the body of the HTTP request, which can be any object.
    /// </summary>
    public object? Body { get; set; }

    /// <summary>
    /// Gets or sets the source of the request, which helps identify the origin of the call.
    /// Defaults to "Unknown".
    /// </summary>
    public string RequestSource { get; set; } = "Unknown";
}
