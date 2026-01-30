using AuthDesktop.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Services;

namespace AuthDesktop.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly IAuthClientService _authClientService;
    private readonly IAuthStateService _authStateService;
    
    public MultiTabViewModel MultiTabViewModel { get; set; }
    public AuthViewModel AuthViewModel { get; set; }
    
    public MainWindowViewModel(IAuthClientService authClientService, IAuthStateService authStateService)
    {
        _authClientService = authClientService;
        _authStateService = authStateService;
        
        MultiTabViewModel = new MultiTabViewModel(_authStateService);
        AuthViewModel = new AuthViewModel(_authClientService, _authStateService);
    }
}