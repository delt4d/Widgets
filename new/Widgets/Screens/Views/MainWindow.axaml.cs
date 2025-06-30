using System;
using Avalonia.Controls;
using Widgets.Screens.ViewModels;
using Widgets.Utils;

namespace Widgets.Screens.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Initialize();
    }

    private void Initialize()
    {
        Opened += OnWindowOpened;

        this.ApplyWindowProperties(new WindowProperties
        {
            ExtendClientArea = true,
            SystemDecorations = SystemDecorations.BorderOnly
        });
    }

    private void OnWindowOpened(object? sender, EventArgs e)
    {
        this.PositionWindow();
    }

    private void CreateWebView()
    {
        var url = "http://127.0.0.1:5500/";
        var widgetWindow = new WidgetWindow(
            new WidgetWindowViewModel
            {
                AddressUrl = url
            })
        {
            ShowInTaskbar = false,
        };
        widgetWindow.Show(this);
    }
}