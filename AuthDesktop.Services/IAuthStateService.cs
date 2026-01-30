using AuthDesktop.Models;

namespace Services;

public interface IAuthStateService
{
    bool IsLoggedIn { get; }

    string? SessionId { get; }

    User? User { get; }

    void Logout();

    void Login(String sessionId, User user);
}