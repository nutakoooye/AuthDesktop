using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AuthDesktop.Models;
using Services;

namespace AuthDesktop.UI.ServicesImpl;

public class AuthClientService : IAuthClientService
{
    private IConfigurationService _configurationService;

    private string _baseUrl;

    private readonly HttpClient _httpClient;


    public AuthClientService(IConfigurationService configurationService, HttpClient? httpClient = null)
    {
        _configurationService = _configurationService =
            configurationService ?? throw new ArgumentNullException(nameof(configurationService));

        _baseUrl = _configurationService.AuthApiUrl;

        
        _httpClient = httpClient ?? new HttpClient();
        if (_httpClient.BaseAddress is null)
            _httpClient.BaseAddress = new Uri(_configurationService.AuthApiUrl.TrimEnd('/') + "/");
    }

    public Task<ApiResult<ApiResponse>> RegisterAsync(User user, CancellationToken ct = default)
        => SendJsonAsync<ApiResponse>(HttpMethod.Post, "user", user, ct);

    public Task<ApiResult<ApiResponse>> LoginAsync(string username, string password, CancellationToken ct = default)
        => SendJsonAsync<ApiResponse>(HttpMethod.Get, $"user/login?username={Uri.EscapeDataString(username)}&password={Uri.EscapeDataString(password)}", body: null, ct);
    
    public Task<ApiResult<User>> GetUserAsync(string username, CancellationToken ct = default)
        => SendJsonAsync<User>(HttpMethod.Get, $"user/{Uri.EscapeDataString(username)}", body: null, ct);

    public Task<ApiResult<ApiResponse>> LogoutAsync(CancellationToken ct = default)
        => SendJsonAsync<ApiResponse>(HttpMethod.Get, "user/logout", body: null, ct);

    private async Task<ApiResult<T>> SendJsonAsync<T>(HttpMethod method, string relativeUrl, object? body,
        CancellationToken ct)
    {
        using var req = new HttpRequestMessage(method, relativeUrl);

        if (body is not null)
        {
            var json = JsonSerializer.Serialize(body);
            req.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        HttpResponseMessage resp;
        string respText;

        try
        {
            resp = await _httpClient.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, ct)
                .ConfigureAwait(false);
        }
        catch (TaskCanceledException ex) when (!ct.IsCancellationRequested)
        {
            return ApiResult<T>.Fail(new ApiError("NetworkError", "Timeout while calling server.", null, ex.Message));
        }
        catch (HttpRequestException ex)
        {
            return ApiResult<T>.Fail(new ApiError("NetworkError", "Network error while calling server.", null,
                ex.Message));
        }
        catch (Exception ex)
        {
            return ApiResult<T>.Fail(new ApiError("Unexpected", "Unexpected error while calling server.", null,
                ex.ToString()));
        }

        try
        {
            respText = await resp.Content.ReadAsStringAsync(ct).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return ApiResult<T>.Fail(
                new ApiError("NetworkError", "Failed to read server response body.", resp.StatusCode, ex.ToString()),
                resp.StatusCode);
        }

        if (!resp.IsSuccessStatusCode)
        {
            var msg = $"HTTP {(int)resp.StatusCode} {resp.ReasonPhrase}".Trim();
            var parsed = TryDeserialize<ApiResponse>(respText, out var apiErr);

            if (parsed)
                msg = $"{msg}. {apiErr.Message}".Trim();

            return ApiResult<T>.Fail(
                new ApiError("HttpError", msg, resp.StatusCode, respText),
                resp.StatusCode
            );
        }
        
        if (string.IsNullOrWhiteSpace(respText))
            return ApiResult<T>.Success(default, resp.StatusCode);

        if (!TryDeserialize<T>(respText, out var data))
        {
            return ApiResult<T>.Fail(
                new ApiError("DeserializeError", "Failed to deserialize JSON response.", resp.StatusCode, respText),
                resp.StatusCode
            );
        }

        return ApiResult<T>.Success(data, resp.StatusCode);
    }

    private bool TryDeserialize<T>(string json, out T? value)
    {
        try
        {
            value = JsonSerializer.Deserialize<T>(json);
            return true;
        }
        catch
        {
            value = default;
            return false;
        }
    }
}