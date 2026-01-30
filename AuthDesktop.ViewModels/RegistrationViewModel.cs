using System.ComponentModel.DataAnnotations;
using AuthDesktop.Models;
using AuthDesktop.ViewModels.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Services;

namespace AuthDesktop.ViewModels;

public partial class RegistrationViewModel : ObservableObject
{
    private readonly IAuthClientService _authClientService;
    
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    private string _username = string.Empty;

    [ObservableProperty] 
    private string _firstName = string.Empty;

    [ObservableProperty] 
    private string _lastName = string.Empty;

    [ObservableProperty] 
    private string _email = string.Empty;

    [ObservableProperty] 
    private string _phoneNumber = string.Empty;

    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    private string _password = string.Empty;
    
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    private string _confirmPassword = string.Empty;

    [ObservableProperty] 
    private string? _errorMessage;

    [ObservableProperty] 
    private bool _isBusy;
    
    [ObservableProperty]
    private string? _validationMessage;

    
    public RegistrationViewModel(IAuthClientService authClientService)
    {
        _authClientService = authClientService;
    }

    
    [RelayCommand(CanExecute = nameof(CanRegister))]
    public async Task RegisterAsync()
    {
        IsBusy = true;
        User user = new User()
        {
            Id = Random.Shared.NextInt64(0, Int64.MaxValue),
            UserName = Username,
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Phone = PhoneNumber,
            Password = Password,
            UserStatus = 0
        };
        var requestResponse = await _authClientService.RegisterAsync(user);

        if (!requestResponse.IsSuccess)
        {
            ErrorMessage = requestResponse.Error?.Message;
            IsBusy = false;
            return;
        }

        if (requestResponse.Data.Code != 200)
        {
            ErrorMessage = requestResponse.Data.Message;
            IsBusy = false;
            return;
        }

        var loginCreds = new LoginCredsViewModel(Username, Password);
        WeakReferenceMessenger.Default.Send(new RegistrationClosedMessage(loginCreds));
        
        Clear();
    }

    
    private bool CanRegister()
    {
        if (string.IsNullOrWhiteSpace(Username) ||
            string.IsNullOrWhiteSpace(Password) ||
            string.IsNullOrWhiteSpace(ConfirmPassword))
        {
            ValidationMessage = "Username ans password is required!";
            return false;
        }

        if (Password != ConfirmPassword)
        {
            ValidationMessage = "Passwords do not match!";
            return false;
        }

        ValidationMessage = null;
        return true;
    }

    private void Clear()
    {
        Username = string.Empty;
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        PhoneNumber = string.Empty;
        Password = string.Empty;
        ErrorMessage = null;
        IsBusy = false;
    }
}