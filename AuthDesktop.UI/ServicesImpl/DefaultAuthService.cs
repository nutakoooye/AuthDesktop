using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AuthDesktop.Models;
using Services;

namespace AuthDesktop.UI.ServicesImpl;

public class DefaultAuthService:IAuthService
{
    private ConfigurationService _configurationService;
    
    public DefaultAuthService(ConfigurationService configurationService)
    {
        _configurationService = configurationService;
    }


    async public Task<long> Login(string username, string password)
    {
        return 0;
    }

    async public Task<long> Register(User user)
    {
        return 0;
    }

    async public Task<bool> Logout()
    {
        return true;
    }

    async public Task<User> GetUser(string username)
    {
        return new User();
    }

    public string GetTest()
    {
        return "Test";
    }
}