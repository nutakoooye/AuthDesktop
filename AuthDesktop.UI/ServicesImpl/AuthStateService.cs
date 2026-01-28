using AuthDesktop.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Services;

namespace AuthDesktop.UI.ServicesImpl;

public partial class AuthStateService : ObservableObject, IAuthStateService
{
    [ObservableProperty]
    private bool _isLoggedIn;

    [ObservableProperty]
    private User? _currentUser;

    public void Logout()
    {
        CurrentUser = null;
        IsLoggedIn = false;
    }

    public void Login(User user)
    {
        CurrentUser = user;
        IsLoggedIn = true;
    }
}