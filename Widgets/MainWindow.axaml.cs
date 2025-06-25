using Avalonia.Controls;
using Avalonia.Interactivity;
using widgets.Features.Timer;
using widgets.Features.Widget;

namespace widgets;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        this.ApplyDefaultWindowProperties(prop =>
        {
            prop.ExtendClientAreaToDecorationsHint = true;
            prop.Transparent = true;
        });
    }

    public void CreateWebviewWindow(object? sender, RoutedEventArgs args)
    {
        var widgetWindow = new WidgetWindow("https://github.com/delt4d/")
        {
            ShowInTaskbar = false
        };
        widgetWindow.Show(this);
    }

    public void CreateTimerWindow(object? sender, RoutedEventArgs args)
    {
        var timerWindow = new TimerWindow();
        timerWindow.Show(this);
    }
}