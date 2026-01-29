using System.Text.Json.Serialization;

namespace AuthDesktop.Models;

public struct User
{
    [JsonPropertyName("id")] public long Id { get; init; }
    [JsonPropertyName("username")] public string? UserName { get; init; }
    [JsonPropertyName("firstName")] public string? FirstName { get; init; }
    [JsonPropertyName("lastName")] public string? LastName { get; init; }
    [JsonPropertyName("email")] public string? Email { get; init; }
    [JsonPropertyName("password")] public string? Password { get; init; }
    [JsonPropertyName("phone")] public string? Phone { get; init; }
    [JsonPropertyName("userStatus")] public int UserStatus { get; init; }
}