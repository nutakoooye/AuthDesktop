using AuthDesktop.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Services;

namespace AuthDesktop.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private IAuthService _authService;
    
    public MultiTabViewModel MultiTabVm { get; set; }
    
    public MainWindowViewModel(IAuthService authService)
    {
        MultiTabVm = new MultiTabViewModel();
        
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