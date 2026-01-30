using AuthDesktop.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Services;

namespace AuthDesktop.UI.ServicesImpl;

public partial class AuthStateService : ObservableObject, IAuthStateService
{
    [ObservableProperty] private bool _isLoggedIn;

    [ObservableProperty] private string? _sessionId;

    [ObservableProperty] private User? _user;

    public void Logout()
    {
        SessionId = null;
        User = null;
        IsLoggedIn = false;
    }

    public void Login(string sessionId, User user)
    {
        SessionId = sessionId;
        User = user;
        IsLoggedIn = true;
    }
}