using Avalonia.Controls;
using Avalonia.Media;
using System;
using Widgets.Helpers;
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

        vm.Widgets.Clear();
        foreach (var widget in SampleWidgetsHelper.GetSampleWidgets())
            vm.Widgets.Add(widget);
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
        BorderThickness = new Avalonia.Thickness(10);
        BorderBrush = Brushes.AliceBlue;
        Padding = new Avalonia.Thickness(0, 25, 0, 0);

        await NodejsServerUtils.EnsureServerRunningAsync();
    }

    private void OnWindowOpened(object? sender, EventArgs e) => this.PositionWindow();
}