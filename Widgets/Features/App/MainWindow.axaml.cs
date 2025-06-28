using Avalonia.Controls;
using widgets.Features.Home;

namespace widgets.Features.App;

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
            homeWindow.Opened += (_, _) => homeWindow.PositionWindow();
            homeWindow.Closed += (_, _) => Close();
            homeWindow.Show();
        };
    }
}