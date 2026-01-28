using AuthDesktop.Models;

namespace Services;

public interface IAuthService
{
    Task<ApiResponse?> LoginAsync(string username, string password);
    
    Task<ApiResponse?> RegisterAsync(User user);
    
    Task<ApiResponse?> LogoutAsync();
    
    Task<User?> GetUserAsync(string username);
    
    bool IsAuthenticated { get; }
    string? CurrentUsername { get; }
}