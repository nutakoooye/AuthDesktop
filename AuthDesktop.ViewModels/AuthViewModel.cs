using CommunityToolkit.Mvvm.ComponentModel;
using Services;

namespace AuthDesktop.ViewModels;

public class AuthViewModel:ObservableObject
{
    private IAuthStateService _authStateService;
    private IAuthClientService _authClientService;
    public LoginViewModel LoginVm { get; set; }
    
    public AuthViewModel(IAuthClientService authClientService, IAuthStateService authStateService)
    {
        _authClientService = authClientService;
        _authStateService = authStateService;
        
        LoginVm = new LoginViewModel(_authClientService, _authStateService);
    }
}