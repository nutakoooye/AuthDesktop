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
    public async Task LoginAsync()
    {
        var loginResponse = await _authClientService.LoginAsync(LoginText, PasswordText);
        
        if (!loginResponse.IsSuccess)
        {
            ErrorMessage =  loginResponse.Error?.Message;
            return;
        }
        if (loginResponse.Data.Code != 200) // /user/login/ может вернуть успешный ответ с кодом 404 в теле ответа
        {
            ErrorMessage = loginResponse.Data.Message;
            return;
        }
        
        var getUserResponse = await _authClientService.GetUserAsync(LoginText); // /user/login/ не проверяет логин и пароль, добавил получение пользователя чтобы удостовериться что такой пользователь существует
        if (!getUserResponse.IsSuccess)
        {
            ErrorMessage =  getUserResponse.Error?.Message;
            return;
        }

        var sessionId = loginResponse.Data.Message.Split(":").Last();
        User user = getUserResponse.Data;
        
        _authStateService.Login(sessionId, user);
        Clear();
    }

    
    private void Clear()
    {
        ErrorMessage = null;
        LoginText = "";
        PasswordText = "";
    }
    
}