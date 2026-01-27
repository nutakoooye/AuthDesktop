using CommunityToolkit.Mvvm.ComponentModel;
using Services;

namespace AuthDesktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel(IAuthService authService)
    {
        _greeting = authService.GetTest();
    }
    
    [ObservableProperty]
    private string _greeting;
}