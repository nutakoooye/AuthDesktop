using System.Net;

namespace AuthDesktop.Models;

public sealed record ApiError(
    string Kind,                 // "HttpError" | "NetworkError" | "DeserializeError" | "Unexpected"
    string Message,
    HttpStatusCode? StatusCode = null,
    string? ResponseBody = null
);