using System.Runtime.CompilerServices;
using AuthDesktop.ViewModels;
using AuthDesktop.ViewModels.Messages;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace AuthDesktop.UI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        if (Design.IsDesignMode)
            return;
        
        WeakReferenceMessenger.Default.Register<MainWindow, RegistrationMessage>(this, static (w, m) =>
        {
            var dialog = App.Services.GetRequiredService<RegistrationWindow>();
            dialog.DataContext = App.Services.GetRequiredService<RegistrationViewModel>();

            m.Reply(dialog.ShowDialog<LogPasViewModel?>(w));
        });
    }
}