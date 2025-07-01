using System;
using Avalonia.Controls;
using Widgets.Controls.ViewModels;
using Widgets.Features;
using Widgets.Features.Widgets;
using Widgets.Screens.ViewModels;
using Widgets.Utils;

namespace Widgets.Screens.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Initialize();

        DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(object? sender, EventArgs e)
    {
        if (DataContext is not MainWindowViewModel vm)
            return;

        var widget1 = new WidgetItemViewModel(new WebServerLauncher
        {
            WidgetWindow = GetWidgetWindow,
            Title = "Webview Widget 1",
            WebServerUrl = "http://localhost:5500/"
        });

        WidgetWindow? widgetWindow = null;
        var widget2 = new WidgetItemViewModel(new WebServerLauncher
        {
            WidgetWindow = () =>
            {
                widgetWindow ??= GetWidgetWindow();
                widgetWindow.Closed += (s, e) => widgetWindow = null;
                return widgetWindow;
            },
            Title = "Webview Widget 2 | SW",
            WebServerUrl = "http://localhost:5500/"
        });

        var widget3 = new WidgetItemViewModel(new LocalWidgetLauncher
        {
            TimerWidgetWindow = () => new TimerWidgetWindow(new TimerWidgetWindowViewModel()),
            Title = "Local Timer Widget"
        });

        vm.Widgets.Add(widget1);
        vm.Widgets.Add(widget2);
        vm.Widgets.Add(widget3);
    }

    private WidgetWindow GetWidgetWindow()
    {
        return new WidgetWindow()
        {
            ShowInTaskbar = false
        };
    }

    private async void Initialize()
    {
        Opened += OnWindowOpened;

        this.ApplyWindowProperties(new WindowProperties
        {
            ExtendClientArea = true,
            SystemDecorations = SystemDecorations.BorderOnly
        });

        await NodejsServerUtils.EnsureServerRunningAsync();
    }

    private void OnWindowOpened(object? sender, EventArgs e) => this.PositionWindow();
}