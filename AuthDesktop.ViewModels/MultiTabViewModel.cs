using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Services;

namespace AuthDesktop.ViewModels;

public partial class MultiTabViewModel: ObservableObject
{
    private IAuthStateService _authStateService;
    public ObservableCollection<Tab> Tabs { get; set; }

    [ObservableProperty] 
    private Tab _selectedTab;
    
    public MultiTabViewModel(IAuthStateService authStateService)
    {
        _authStateService = authStateService;
        
        Tabs =
        [
            new Tab("User Info", new UserInfoViewModel(_authStateService)),
            new Tab("Developer page", new DeveloperDataViewModel())
        ];

        SelectedTab = Tabs[0];
    }
}