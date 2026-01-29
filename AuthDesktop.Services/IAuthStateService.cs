using AuthDesktop.Models;

namespace Services;

public interface IAuthStateService
{
    bool IsLoggedIn { get;  }
    
    string? SessionId { get; }
    
    string? UserName { get; }
    
    void Logout();
    
    void Login(String sessionId,  String userName);
}