using System.Text.Json.Serialization;

namespace AuthDesktop.Models;

public struct ApiResponse
{
    [JsonPropertyName("code")] public int Code { get; init; }
    [JsonPropertyName("type")] public string? Type { get; init; }
    [JsonPropertyName("message")] public string Message { get; init; }
}