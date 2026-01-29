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
    
    [ObservableProperty]
    private string? _errorMessage;

    public LoginViewModel(IAuthClientService authClientService, IAuthStateService authStateService)
    {
        _authClientService = authClientService;
        _authStateService = authStateService;
    }
    
    
    [RelayCommand]
    public async Task LoginCommand()
    {
        var loginResponse = await _authClientService.LoginAsync(LoginText, PasswordText);
        if (loginResponse.IsSuccess)
        {
            if (loginResponse.Data.Code == 200)
            {
                var sessionId = loginResponse.Data.Message.Split(":").Last();
                _authStateService.Login(LoginText, sessionId);
            }
            else
            {
                ErrorMessage = loginResponse.Data.Message;
            }
        }
        else
        {
            ErrorMessage =  loginResponse.Error?.Message;
        }
    }
}