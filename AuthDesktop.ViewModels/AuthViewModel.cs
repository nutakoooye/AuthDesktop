using System.Diagnostics;
using AuthDesktop.Models;
using AuthDesktop.ViewModels.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Services;

namespace AuthDesktop.ViewModels;

public partial class AuthViewModel:ObservableObject
{
    [ObservableProperty]
    private IAuthStateService _authStateService;
    
    private IAuthClientService _authClientService;
    public LoginViewModel LoginVm { get; set; }
    
    [ObservableProperty]
    private string? _errorMessage;
    
    public AuthViewModel(IAuthClientService authClientService, IAuthStateService authStateService)
    {
        _authClientService = authClientService;
        _authStateService = authStateService;
        
        LoginVm = new LoginViewModel(_authClientService, _authStateService);
    }
    
    [RelayCommand]
    public async Task LogoutAsync()
    {
        var logoutResponse = await _authClientService.LogoutAsync();
        
        if (!logoutResponse.IsSuccess)
        {
            ErrorMessage =  logoutResponse.Error?.Message;
            return;
        }
        if (logoutResponse.Data.Code != 200)
        {
            ErrorMessage = logoutResponse.Data.Message;
            return;
        }
        
        AuthStateService.Logout();
    }

    [RelayCommand]
    public async Task RegisterAsync()
    {
        var loginCreds = await WeakReferenceMessenger.Default.Send(new RegistrationMessage());
        if (loginCreds is not null)
        {
            LoginVm.LoginText = loginCreds.Login;
            LoginVm.PasswordText = loginCreds.Password;
            LoginVm.LoginCommand.Execute(null);
        }
    }
}