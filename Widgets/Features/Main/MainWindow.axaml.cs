using Avalonia.Controls;
using widgets.Features.Home;

namespace widgets.Features.Main;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        ShowInTaskbar = true;
        Opened += (sender, e) =>
        {
            Hide();
            var homeWindow = new HomeWindow();
            homeWindow.Opened += (sender, e) => homeWindow.PositionWindow();
            homeWindow.Show();
        };
    }
}