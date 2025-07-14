using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Widgets.UI.ViewModels;

namespace Widgets.UI.Views;

public partial class WidgetItemControl : UserControl<WidgetItemViewModel>
{
    public WidgetItemControl()
    {
        InitializeComponent();
    }

    public async void ActivateWidgetClicked(object? sender, RoutedEventArgs args)
    {
        await ViewModel.WidgetLauncher.ExecuteAsync();
    }

    public void ConfigureWidgetClicked(object? sender, RoutedEventArgs args)
    {
        var window = new WidgetSettingsWindow();
        window.ShowDialog<MainWindow>(App.MainWindow);
    }

    public void DeleteWidgetClicked(object? sender, RoutedEventArgs args)
    {
        ViewModel.DeleteEvent?.Invoke(this, EventArgs.Empty);
    }
}