using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using Widgets.Screens.Views;

namespace Widgets.ViewModels;

public partial class AppViewModel
{
    public required MainWindow MainWindow { get; set; }
    public ICommand ShowMainWindow { get; set; }

    public AppViewModel()
    {
        ShowMainWindow = new RelayCommand(() =>
        {
            MainWindow.Show();
            MainWindow.Activate();
        });
    }
}

