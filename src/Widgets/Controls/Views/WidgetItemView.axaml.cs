using System;
using Avalonia.Interactivity;
using Widgets.Controls.ViewModels;
using Widgets.Utils;

namespace Widgets.Controls.Views;

public partial class WidgetItemView : UserControl<WidgetItemViewModel>
{
    public WidgetItemView()
    {
        InitializeComponent();
    }

    public async void OnActivateWidgetClicked(object? sender, RoutedEventArgs args)
    {
        await ViewModel.WidgetLauncher.ExecuteAsync();
    }

    public void OnDeleteWidgetClicked(object? sender, RoutedEventArgs args)
    {
        ViewModel.OnRemoveRequested?.Invoke(this, EventArgs.Empty);
    }
}