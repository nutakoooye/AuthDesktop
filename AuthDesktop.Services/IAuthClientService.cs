using AuthDesktop.Models;

namespace Services;

public interface IAuthClientService
{
    Task<ApiResult<ApiResponse>> RegisterAsync(User user, CancellationToken ct = default);
    Task<ApiResult<ApiResponse>> LoginAsync(string username, string password, CancellationToken ct = default);
    Task<ApiResult<User>> GetUserAsync(string username, CancellationToken ct = default);
    Task<ApiResult<ApiResponse>> LogoutAsync(CancellationToken ct = default);
}