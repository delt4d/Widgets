using CommunityToolkit.Mvvm.Input;
using System;
using Widgets.Screens.Views;

namespace Widgets.ViewModels;

public partial class AppViewModel : ViewModelBase
{
    public EventHandler? Exit { get; set; }

    public required MainWindow MainWindow { get; set; }

    [RelayCommand]
    public void ShowMainWindow()
    {
        MainWindow.Show();
        MainWindow.Activate();
    }

    [RelayCommand]
    public void ExitCommand()
    {
        Exit?.Invoke(this, EventArgs.Empty);
    }
}

