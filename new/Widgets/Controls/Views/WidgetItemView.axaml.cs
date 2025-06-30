using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Widgets.Controls.Views;

public partial class WidgetItemView : UserControl
{
    public static readonly StyledProperty<Action?> OnWidgetActivatedActionProperty =
        AvaloniaProperty.Register<WidgetItemView, Action?>(nameof(OnWidgetActivatedAction));

    public Action? OnWidgetActivatedAction
    {
        get => GetValue(OnWidgetActivatedActionProperty);
        set => SetValue(OnWidgetActivatedActionProperty, value);
    }

    public WidgetItemView()
    {
        InitializeComponent();
    }

    private void OnActivateWidgetClicked(object? sender, RoutedEventArgs e)
    {
        OnWidgetActivatedAction?.Invoke();
    }
}