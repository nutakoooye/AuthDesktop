using System.Net;

namespace AuthDesktop.Models;

public sealed record ApiError(
    string Kind,
    string Message,
    HttpStatusCode? StatusCode = null,
    string? ResponseBody = null
);