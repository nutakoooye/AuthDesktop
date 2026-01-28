using AuthDesktop.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Services;

namespace AuthDesktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private IAuthService _authService;
    public MainWindowViewModel(IAuthService authService)
    {
        _authService = authService;
        
        _greeting = "Greeting";

        //TestApi().Wait();
        Task.Run(TestApi);
    }

    private async Task TestApi()
    {
        var response =  await _authService.LoginAsync("admin", "admin");
        if (response is { } resp)
        {
            Greeting = resp.Message;
        }
    }
    
    [ObservableProperty]
    private string _greeting;
}