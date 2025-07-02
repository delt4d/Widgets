using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Widgets.Screens.Views;

namespace Widgets.ViewModels;

public partial class AppViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool shouldExit;

    public required MainWindow MainWindow { get; set; }

    [RelayCommand]
    public void ShowMainWindow()
    {
        MainWindow.Show();
        MainWindow.Activate();
    }

    [RelayCommand]
    public void Exit()
    {
        ShouldExit = true;
    }
}

