using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AuthDesktop.ViewModels;

public partial class MultiTabViewModel: ObservableObject
{
    public ObservableCollection<Tab> Tabs { get; set; }

    [ObservableProperty] 
    private Tab? _selectedTab;
    
    public MultiTabViewModel()
    {
        Tabs = new();
        Tabs.Add(new Tab("User Info", new UserInfoViewModel()));
        Tabs.Add(new Tab("Developer page", new DeveloperDataViewModel()));
    }
}