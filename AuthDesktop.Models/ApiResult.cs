using System.Net;

namespace AuthDesktop.Models;

public sealed record ApiResult<T>(
    bool IsSuccess,
    T? Data,
    ApiError? Error,
    HttpStatusCode? StatusCode = null)
{
    public static ApiResult<T> Success(T? data, HttpStatusCode? statusCode = null)
        => new(true, data, null, statusCode);

    public static ApiResult<T> Fail(ApiError error, HttpStatusCode? statusCode = null)
        => new(false, default, error, statusCode);
}