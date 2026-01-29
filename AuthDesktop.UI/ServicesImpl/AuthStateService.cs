using AuthDesktop.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Services;

namespace AuthDesktop.UI.ServicesImpl;

public partial class AuthStateService : ObservableObject, IAuthStateService
{
    [ObservableProperty]
    private bool _isLoggedIn;

    [ObservableProperty]
    private string? _sessionId;
    
    [ObservableProperty]
    private string? _userName;

    public void Logout()
    {
        SessionId = null;
        UserName = null;
        IsLoggedIn = false;
    }

    public void Login(string sessionId,   string userName)
    {
        SessionId = sessionId;
        UserName = userName;
        IsLoggedIn = true;
    }
}