using Avalonia.Controls;
using Avalonia.Interactivity;
using Widgets.Controls.ViewModels;

namespace Widgets.Controls.Views;

public partial class WidgetItemView : UserControl
{
    public WidgetItemView()
    {
        InitializeComponent();
    }

    public async void OnActivateWidgetClicked(object? sender, RoutedEventArgs args)
    {
        if (DataContext is not WidgetItemViewModel vm)
            return;

        await vm.WidgetLauncher.ExecuteAsync();
    }
}