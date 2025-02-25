using System.Net;
using System.Text.Json.Serialization;

namespace DotnetSkeleton.SharedKernel.Utils.Models.Responses;

/// <summary>
/// Represents a base response object that can be used to return standardized responses from API endpoints.
/// </summary>
public class BaseResponse()
{
    private BaseResponse(int status, string? code, string? message, object? data = null, object? error = null) : this()
    {
        Status = status;
        Code = code;
        Message = message;
        Data = data;
        Errors = error;
    }

    /// <summary>
    /// Gets or sets the status code of the response.
    /// </summary>
    [JsonPropertyName("status")]
    public int Status { get; set; }

    /// <summary>
    /// Gets or sets the code associated with the response.
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    /// <summary>
    /// Gets or sets the message associated with the response.
    /// </summary>
    [JsonPropertyName("message")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; set; }

    /// <summary>
    /// Gets or sets the data associated with the response.
    /// </summary>
    [JsonPropertyName("data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Data { get; set; }

    /// <summary>
    /// Gets or sets the error associated with the response.
    /// </summary>
    [JsonPropertyName("errors")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Errors { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="BaseResponse"/> class with a status code of 200 (OK).
    /// </summary>
    /// <param name="data">The data to include in the response.</param>
    /// <returns>A new instance of the <see cref="BaseResponse"/> class.</returns>
    public static BaseResponse Ok(object? data = null)
    {
        return new BaseResponse(
            (int)HttpStatusCode.OK,
            HttpStatusCode.OK.ToString(),
            null,
            data,
            null);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="BaseResponse"/> class with a status code of 201 (Created).
    /// </summary>
    /// <param name="data">The data to include in the response.</param>
    /// <returns>A new instance of the <see cref="BaseResponse"/> class.</returns>
    public static BaseResponse Created(object? data = null)
    {
        return new BaseResponse(
            (int)HttpStatusCode.Created,
            HttpStatusCode.Created.ToString(),
            null,
            data,
            null);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="BaseResponse"/> class with a status code of 500 (Internal Server Error).
    /// </summary>
    /// <param name="message">The error message to include in the response.</param>
    /// <param name="error">The error object to include in the response.</param>
    /// <returns>A new instance of the <see cref="BaseResponse"/> class.</returns>
    public static BaseResponse ServerError(string? message = "An unexpected server error occurred, please try again.", object? error = null)
    {
        return new BaseResponse(
            (int)HttpStatusCode.InternalServerError,
            HttpStatusCode.InternalServerError.ToString(),
            message,
            null,
            error);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="BaseResponse"/> class with a status code of 400 (Bad Request).
    /// </summary>
    /// <param name="message">The error message to include in the response.</param>
    /// <param name="error">The error object to include in the response.</param>
    /// <returns>A new instance of the <see cref="BaseResponse"/> class.</returns>
    public static BaseResponse BadRequest(string? message = "The request is invalid, please check your input.", object? error = null)
    {
        return new BaseResponse(
            (int)HttpStatusCode.BadRequest,
            HttpStatusCode.BadRequest.ToString(),
            message,
            null,
            error);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="BaseResponse"/> class with a status code of 404 (Not Found).
    /// </summary>
    /// <param name="message">The error message to include in the response.</param>
    /// <param name="error">The error object to include in the response.</param>
    /// <returns>A new instance of the <see cref="BaseResponse"/> class.</returns>
    public static BaseResponse NotFound(string? message = "The requested resource was not found.", object? error = null)
    {
        return new BaseResponse(
            (int)HttpStatusCode.NotFound,
            HttpStatusCode.NotFound.ToString(),
            message,
            null,
            error);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="BaseResponse"/> class with a status code of 401 (Unauthorized).
    /// </summary>
    /// <param name="message">The error message to include in the response.</param>
    /// <param name="error">The error object to include in the response.</param>
    /// <returns>A new instance of the <see cref="BaseResponse"/> class.</returns>
    public static BaseResponse Unauthorized(string? message = "You are not authorized to access this resource.", object? error = null)
    {
        return new BaseResponse(
            (int)HttpStatusCode.Unauthorized,
            HttpStatusCode.Unauthorized.ToString(),
            message,
            null,
            error);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="BaseResponse"/> class with a status code of 403 (Forbidden).
    /// </summary>
    /// <param name="message">The error message to include in the response.</param>
    /// <param name="error">The error object to include in the response.</param>
    /// <returns>A new instance of the <see cref="BaseResponse"/> class.</returns>
    public static BaseResponse Forbidden(string? message = "You are not allowed to access this resource.", object? error = null)
    {
        return new BaseResponse(
            (int)HttpStatusCode.Forbidden,
            HttpStatusCode.Forbidden.ToString(),
            message,
            null,
            error);
    }
}