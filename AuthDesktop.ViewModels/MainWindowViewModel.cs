using AuthDesktop.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Services;

namespace AuthDesktop.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private IAuthClientService _authClientService;
    private IAuthStateService _authStateService;
    
    public MultiTabViewModel MultiTabVm { get; set; }
    public AuthViewModel AuthVm { get; set; }
    
    public MainWindowViewModel(IAuthClientService authClientService, IAuthStateService authStateService)
    {
        _authClientService = authClientService;
        _authStateService = authStateService;
        
        MultiTabVm = new MultiTabViewModel(_authStateService);
        AuthVm = new AuthViewModel(_authClientService, _authStateService);
    }
}