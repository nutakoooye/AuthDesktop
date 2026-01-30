using System;
using AuthDesktop.UI.ServicesImpl;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AuthDesktop.ViewModels;
using AuthDesktop.UI;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace AuthDesktop.UI;

public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = default!;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // If you use CommunityToolkit, line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        // Register all the services needed for the application to run
        var collection = new ServiceCollection();
        collection.AddCommonServices();

        // Creates a ServiceProvider containing services from the provided IServiceCollection
        Services = collection.BuildServiceProvider();

        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = Services.GetRequiredService<MainWindow>();
            desktop.MainWindow.DataContext = Services.GetRequiredService<MainWindowViewModel>();
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = Services.GetRequiredService<MainWindow>();
            singleViewPlatform.MainView.DataContext = Services.GetRequiredService<MainWindowViewModel>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}


public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection collection)
    {
        collection.AddSingleton<IAuthClientService, AuthClientService>();
        collection.AddSingleton<IAuthStateService, AuthStateService>();
        collection.AddSingleton<IConfigurationService, ConfigurationService>();
        
        collection.AddTransient<RegistrationViewModel>();
        collection.AddTransient<RegistrationWindow>();
        
        collection.AddTransient<MainWindowViewModel>();
        collection.AddTransient<MainWindow>();
    }
}