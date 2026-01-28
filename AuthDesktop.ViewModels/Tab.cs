using CommunityToolkit.Mvvm.ComponentModel;

namespace AuthDesktop.ViewModels;

public class Tab
{
    public string Title { get; set; }
    
    public ObservableObject ViewModel { get; set; }

    public Tab(string title, ObservableObject viewModel)
    {
        Title = title;
        ViewModel = viewModel;
    }
}