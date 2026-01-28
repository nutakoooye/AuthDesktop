using AuthDesktop.Models;

namespace Services;

public interface IAuthStateService
{
    bool IsLoggedIn { get;  }
    
    User? CurrentUser { get; }
    
    void Logout();
    
    void Login(User user);
}