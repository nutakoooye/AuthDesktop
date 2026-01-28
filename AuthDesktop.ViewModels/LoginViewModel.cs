using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Services;

namespace AuthDesktop.ViewModels;

public partial class LoginViewModel:ObservableObject
{
    private IAuthClientService  _authClientService;
    private IAuthStateService _authStateService;
    
    [ObservableProperty]
    private string _loginText;
    
    [ObservableProperty]
    private string _passwordText;

    public LoginViewModel(IAuthClientService authClientService, IAuthStateService authStateService)
    {
        _authClientService = authClientService;
        _authStateService = authStateService;
    }
    
    
    [RelayCommand]
    public async Task LoginCommand()
    {
        var response = await _authClientService.LoginAsync(LoginText, PasswordText);
        if (response is null || response.Value.Code != 200)
            return; 
        var user = await _authClientService.GetUserAsync(LoginText);
        if (user is {} u)
        {
            _authStateService.Login(u);
        }
    }
}