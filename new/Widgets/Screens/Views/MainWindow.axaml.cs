using System;
using Avalonia.Controls;
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

    private void OnWindowOpened(object? sender, EventArgs e) => this.PositionWindow();
}