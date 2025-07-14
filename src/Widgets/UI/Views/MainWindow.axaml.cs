using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System;
using Widgets.UI.ViewModels;
using Widgets.Utils;

namespace Widgets.UI.Views;

public partial class MainWindow : Window<MainViewModel>
{
    public MainWindow() : base(new MainViewModel())
    {
        InitializeComponent();
        Initialize();
    }

    public MainWindow(MainViewModel vm) : base(vm)
    {
        InitializeComponent();
        Initialize();
    }

    private async void Initialize()
    {
        Opened += OnWindowOpened;

        this.ApplyWindowProperties(new WindowProperties
        {
            ExtendClientArea = true,
            SystemDecorations = SystemDecorations.Full,
            WindowStyles = WindowStyles.Acrylic
        });
        BorderThickness = new Thickness(10);
        BorderBrush = Brushes.AliceBlue;
        Padding = new Thickness(0, 25, 0, 0);

        await NodejsServerUtils.EnsureServerRunningAsync();
    }

    private void OnWindowOpened(object? sender, EventArgs e) => this.PositionWindow();
}