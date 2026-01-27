using AuthDesktop.Models;

namespace Services;

public interface IAuthService
{
    Task<long> Login(string username, string password);
    
    Task<long> Register(User user);
    
    Task<bool> Logout();
    
    Task<User> GetUser(string username);
    
    string GetTest();
}