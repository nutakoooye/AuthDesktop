using System.ComponentModel;
using AuthDesktop.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Services;

namespace AuthDesktop.ViewModels;

public partial class UserInfoViewModel:ObservableObject
{
    [ObservableProperty] private IAuthStateService _authStateService;
    
    [ObservableProperty] private User _user;


    public UserInfoViewModel(IAuthStateService authStateService)
    {
        AuthStateService = authStateService;
        
        if (_authStateService is INotifyPropertyChanged inpc)
            inpc.PropertyChanged += AuthStateOnPropertyChanged;
    }
    
    private void AuthStateOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AuthStateService.User) || e.PropertyName == nameof(IAuthStateService.User))
        {
            if (AuthStateService.User is { } user)
            {
                User = user;
            }
        }
    }

}