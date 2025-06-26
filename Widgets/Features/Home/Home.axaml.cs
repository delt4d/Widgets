using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using widgets.Features.Timer;
using widgets.Features.Widget;

namespace widgets.Features.Home;

public partial class HomeWindow : Window
{
    public HomeWindow()
    {
        InitializeComponent();

        this.ApplyWidgetDefaultProperties();
        this.ApplyDefaultWindowProperties(prop =>
        {
            prop.ExtendClientAreaToDecorationsHint = true;
            prop.Transparent = true;
        });

        SystemDecorations = SystemDecorations.BorderOnly;
        CornerRadius = new CornerRadius(0);
    }

    private void CreateWebviewWindow(object? sender, RoutedEventArgs args)
    {
        var widgetWindow = new WidgetWindow("http://127.0.0.1:5500/"){ ShowInTaskbar = false };
        widgetWindow.Show(this);
    }

    private void CreateTimerWindow(object? sender, RoutedEventArgs args)
    {
        var timerWindow = new TimerWindow();
        timerWindow.Show(this);
    }
}