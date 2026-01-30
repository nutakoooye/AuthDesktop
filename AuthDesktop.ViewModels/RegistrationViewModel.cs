using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Services;

namespace AuthDesktop.ViewModels;

public partial class RegistrationViewModel : ObservableObject
{
    private IAuthClientService _authClientService;

    [ObservableProperty] 
    private string _username;
    
    [ObservableProperty] 
    private string _firstName;
    
    [ObservableProperty] 
    private string _lastName;
    
    [ObservableProperty] 
    private string _email;
    
    [ObservableProperty] 
    private string _phoneNumber;
    
    [ObservableProperty] 
    private string _password;
    
    public RegistrationViewModel(IAuthClientService authClientService)
    {
        _authClientService = authClientService;
    }

    [RelayCommand]
    public async Task RegisterAsync()
    {

    }
}