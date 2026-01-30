using AuthDesktop.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Services;

namespace AuthDesktop.ViewModels;

public partial class LoginViewModel:ObservableObject
{
    private IAuthClientService  _authClientService;
    private IAuthStateService _authStateService;
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    private string _loginText = string.Empty;
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    private string _passwordText  = string.Empty;
    
    [ObservableProperty]
    private string? _errorMessage;
    
    [ObservableProperty] 
    private bool _isBusy;

    public LoginViewModel(IAuthClientService authClientService, IAuthStateService authStateService)
    {
        _authClientService = authClientService;
        _authStateService = authStateService;
    }
    
    
    [RelayCommand(CanExecute = nameof(CanLogin))]
    private async Task LoginAsync() 
    {
        IsBusy = true;
        var loginResponse = await _authClientService.LoginAsync(LoginText, PasswordText);
        
        if (!loginResponse.IsSuccess)
        {
            ErrorMessage =  loginResponse.Error?.Message;
            IsBusy = false;
            return;
        }
        if (loginResponse.Data.Code != 200) 
        {
            ErrorMessage = loginResponse.Data.Message;
            IsBusy = false;
            return;
        }
        
        var getUserResponse = await _authClientService.GetUserAsync(LoginText); // /user/login/ не проверяет логин и пароль, добавил получение пользователя чтобы удостовериться что такой пользователь существует
        if (!getUserResponse.IsSuccess)
        {
            ErrorMessage =  getUserResponse.Error?.Message;
            IsBusy = false;
            return;
        }

        var sessionId = loginResponse.Data.Message.Split(":").Last();
        User user = getUserResponse.Data;
        
        _authStateService.Login(sessionId, user);
        Clear();
        IsBusy = false;
    }

    
    private void Clear()
    {
        ErrorMessage = null;
        LoginText = "";
        PasswordText = "";
    }

    private bool CanLogin() => LoginText != string.Empty && PasswordText != string.Empty;
    
}