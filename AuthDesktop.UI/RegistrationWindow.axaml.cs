using System;
using AuthDesktop.ViewModels;
using AuthDesktop.ViewModels.Messages;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;

namespace AuthDesktop.UI;

public partial class RegistrationWindow : Window
{
    public RegistrationWindow()
    {
        InitializeComponent(); ;
        
        WeakReferenceMessenger.Default.Register<RegistrationWindow, RegistrationClosedMessage>(this,
            static (w, m) => w.Close(m.LoginCreds));
        
    }
}