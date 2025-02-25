using DotnetSkeleton.SharedKernel.Utils.Models.Requests;
using System.Text.Json;
using System.Text;
using System.Web;
using Microsoft.Extensions.Logging;

namespace DotnetSkeleton.SharedKernel.Utils;

public class HttpClientHelper: IDisposable
{
    private static readonly HttpClient _httpClient = new();

    /// <summary>
    /// Asynchronously sends an HTTP request using the specified parameters and deserializes the response into a specified type.
    /// Use a try-catch block to handle specific issues that may occur during the process of building, sending, and receiving an HTTP request.
    /// </summary>
    /// <typeparam name="TResponse">The type to which the response content will be deserialized.</typeparam>
    /// <param name="requestParameter">The parameters for the HTTP request, including method, URL, headers, and body.</param>
    /// <param name="logger">The logger of the service caller</param>
    /// <returns>
    /// An instance of <typeparamref name="TResponse"/> containing the deserialized response content if the 
    /// request is successful; otherwise, returns the default value for <typeparamref name="TResponse"/>.
    /// </returns>
    public static async Task<TResponse?> SendHttpRequestAsync<TResponse>(BaseHttpRequestParameter requestParameter, ILogger logger)
    {
        try
        {
            // Step 1. Clear and add a custom header to identify the request resource
            _httpClient.DefaultRequestHeaders.Remove(Constant.HeaderAttribute.XRequestSource);
            _httpClient.DefaultRequestHeaders.Add(Constant.HeaderAttribute.XRequestSource,
                requestParameter.RequestSource);

            // Step 2. Build the full URL with query params if having
            var urlBuilder =
                new UriBuilder($"{requestParameter.BaseUrl.TrimEnd('/')}/{requestParameter.Endpoint.TrimStart('/')}");
            if (requestParameter.QueryParams is { Count: > 0 })
            {
                var query = HttpUtility.ParseQueryString(string.Empty);
                foreach (var queryParam in requestParameter.QueryParams.Where(queryParam =>
                             !string.IsNullOrEmpty(queryParam.Value)))
                {
                    query[queryParam.Key] = queryParam.Value;
                }

                urlBuilder.Query = query.ToString();
            }

            using var request = new HttpRequestMessage(requestParameter.Method, urlBuilder.ToString());

            // Step 3. Add request header if provided
            if (requestParameter.Headers is { Count: > 0 })
            {
                foreach (var header in requestParameter.Headers.Where(header => !string.IsNullOrEmpty(header.Value)))
                {
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            // Step 4. Add request file if provided
            if (requestParameter.Files is { Count: > 0 })
            {
                var multiContent = new MultipartFormDataContent();
                foreach (var file in requestParameter.Files)
                {
                    multiContent.Add(new StreamContent(file.Value), file.Key, file.Key);
                }

                if (requestParameter.Body != null)
                {
                    var jsonContent = JsonSerializer.Serialize(requestParameter.Body);
                    multiContent.Add(
                        new StringContent(jsonContent, Encoding.UTF8, Constant.ContentType.ApplicationJson),
                        Constant.ContentType.Json);
                }

                request.Content = multiContent;
            }

            // Step 5. Add request body if provided
            if (requestParameter.Body != null && IsMethodSupportBody(requestParameter.Method) &&
                requestParameter.Files == null)
            {
                var jsonContent = JsonSerializer.Serialize(requestParameter.Body);
                request.Content = new StringContent(jsonContent, Encoding.UTF8, Constant.ContentType.ApplicationJson);
            }

            // Step 6. Send the HTTP request using HttpClient
            logger.LogInformation("Start request {url}, with method {method}"
                ,request.RequestUri?.ToString()
                , request.Method);
            using var response = await _httpClient.SendAsync(request);

            // Step 7. Read and deserialize response content
            var responseData = await response.Content.ReadAsStringAsync();
            return string.IsNullOrEmpty(responseData)
                ? default
                : JsonSerializer.Deserialize<TResponse>(responseData) ?? default;
        }
        catch (JsonException ex)
        {
            logger.LogError(ex, "Failed to deserialize response to {Type}", typeof(TResponse));
            return default;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while sending the HTTP request");
            throw;
        }
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }

    private static bool IsMethodSupportBody(HttpMethod method) =>
        method == HttpMethod.Post || method == HttpMethod.Put || method == HttpMethod.Delete;
}