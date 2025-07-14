using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using System.Linq;
using Widgets.UI.ViewModels;

namespace Widgets.UI.Views;

public partial class App : Application
{
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

            var mainWindow = new MainWindow()
            {
                ClosingBehavior = Avalonia.Controls.WindowClosingBehavior.OwnerWindowOnly
            };
            mainWindow.Closing += (s,e) =>
            {
                e.Cancel = true;
                mainWindow.Hide();
            };

            ViewModel = new AppViewModel()
            {
                Exit = (s, e) =>
                {
                    desktop.Shutdown(0);
                },
                ShowMainWindow = (s, e) =>
                {
                    mainWindow.Show();
                    mainWindow.Activate();
                }
            };

            desktop.ShutdownMode = Avalonia.Controls.ShutdownMode.OnExplicitShutdown;
            desktop.MainWindow = mainWindow;
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