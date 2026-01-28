using CommunityToolkit.Mvvm.ComponentModel;

namespace AuthDesktop.ViewModels;

public partial class DeveloperDataViewModel:ObservableObject
{
    [ObservableProperty]
    private string _name;
    
    [ObservableProperty]
    private string _email;
    
    [ObservableProperty]
    private string _site;
    
    [ObservableProperty]
    private string _copyrights;

    [ObservableProperty] 
    private string _license;
    
    [ObservableProperty]
    private string _version;

    [ObservableProperty] 
    private string _aboutProgram;
}