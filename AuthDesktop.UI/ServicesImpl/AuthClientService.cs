using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AuthDesktop.Models;
using Services;

namespace AuthDesktop.UI.ServicesImpl;

public class AuthClientService : IAuthClientService
{
    private IConfigurationService _configurationService;

    private string _baseUrl;

    private readonly HttpClient _httpClient;

    private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };


    public AuthClientService(IConfigurationService configurationService)
    {
        _configurationService = configurationService;
        _baseUrl = _configurationService.AuthApiUrl;
        _httpClient = new HttpClient();
    }


    public async Task<ApiResponse?> RegisterAsync(User user)
    {
        var json = JsonSerializer.Serialize(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{_baseUrl}/user", content);
        if (!response.IsSuccessStatusCode)
            return null;

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ApiResponse>(responseJson, _jsonOptions);
    }


    public async Task<ApiResponse?> LoginAsync(string username, string password)
    {
        var url = $"{_baseUrl}/user/login?username={username}&password={password}";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return null;

        var responseJson = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonSerializer.Deserialize<ApiResponse>(responseJson, _jsonOptions);

        return apiResponse;
    }


    public async Task<User?> GetUserAsync(string username)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/user/{username}");
        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<User>(json, _jsonOptions);
    }


    public async Task<ApiResponse?> LogoutAsync()
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/user/logout");
        if (!response.IsSuccessStatusCode)
            return null;

        var responseJson = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonSerializer.Deserialize<ApiResponse>(responseJson, _jsonOptions);

        return apiResponse;
    }
}