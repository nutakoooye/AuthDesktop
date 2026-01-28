using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Services;

namespace AuthDesktop.ViewModels;

public partial class UserInfoViewModel:ObservableObject
{
    public IAuthStateService AuthStateService {get; init;}
    

    public UserInfoViewModel(IAuthStateService authStateService)
    {
        AuthStateService = authStateService;
    }
}