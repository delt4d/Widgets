using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using System.Linq;
using Widgets.UI.ViewModels;

namespace Widgets.UI.Views;

public partial class App : Application
{
    public static Window MainWindow { get; private set; } = default!;
    
    public AppViewModel ViewModel
    {
        get => (AppViewModel)DataContext!;
        set => DataContext = value;
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();

            MainWindow = new MainWindow()
            {
                ClosingBehavior = WindowClosingBehavior.OwnerWindowOnly
            };
            MainWindow.Closing += (s,e) =>
            {
                e.Cancel = true;
                MainWindow.Hide();
            };

            ViewModel = new AppViewModel()
            {
                Exit = (s, e) =>
                {
                    desktop.Shutdown(0);
                },
                ShowMainWindow = (s, e) =>
                {
                    MainWindow.Show();
                    MainWindow.Activate();
                }
            };

            desktop.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            desktop.MainWindow = MainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}