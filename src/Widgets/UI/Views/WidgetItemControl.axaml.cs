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

    public async void OnActivateWidgetClicked(object? sender, RoutedEventArgs args)
    {
        await ViewModel.WidgetLauncher.ExecuteAsync();
    }

    public void OnDeleteWidgetClicked(object? sender, RoutedEventArgs args)
    {
        ViewModel.DeleteEvent?.Invoke(this, EventArgs.Empty);
    }
}