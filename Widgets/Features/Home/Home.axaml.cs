using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using widgets.Features.Timer;
using widgets.Features.Widget.Components;
using widgets.Features.Widget.Windows;

namespace widgets.Features.Home;

public partial class HomeWindow : Window
{
    private readonly List<WidgetPanelControl> _widgets = [];

    public HomeWindow()
    {
        InitializeComponent();

        Width = 250;
        SizeToContent = SizeToContent.Height;
        SystemDecorations = SystemDecorations.BorderOnly;

        this.ApplyDefaultWindowProperties(prop =>
        {
            prop.ExtendClientAreaToDecorationsHint = true;
            prop.Transparent = false;
        });

        _widgets.Add(new WidgetPanelControl(new WidgetPanelControlParams("Timer", CreateTimerWindow)));
        _widgets.Add(new WidgetPanelControl(new WidgetPanelControlParams("Webview", CreateWebviewWindow)));

        UpdateWidgetsList();
    }

    public void UpdateWidgetsList()
    {
        WidgetsPanel.Children.Clear();
        WidgetsPanel.Children.AddRange(_widgets);
    }

    private void CreateWebviewWindow(object? sender, RoutedEventArgs args)
    {
        var widgetWindow = new WidgetWindow("http://127.0.0.1:5500/") { ShowInTaskbar = false };
        widgetWindow.Show(this);
    }

    private void CreateTimerWindow(object? sender, RoutedEventArgs args)
    {
        var timerWindow = new TimerWindow();
        timerWindow.Show(this);
    }

    public void PositionWindow()
    {
        var screenSize = Screens.Primary!.WorkingArea.Size;
        var windowSize = PixelSize.FromSize(ClientSize, Screens.Primary.Scaling);
        Position = new PixelPoint(
            screenSize.Width - windowSize.Width - 3,
            screenSize.Height - windowSize.Height - 3);
    }
}