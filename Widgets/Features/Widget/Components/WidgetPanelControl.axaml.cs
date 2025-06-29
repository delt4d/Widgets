using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace widgets.Features.Widget.Components;

public record WidgetPanelControlParams(
    string Title,
    Action<object?, RoutedEventArgs> OnActivate
);

public partial class WidgetPanelControl : UserControl
{
    public string Title
    {
        get => WidgetText.Text?.ToString() ?? string.Empty;
        set => WidgetText.Text = value;
    }

    public WidgetPanelControl()
    {
        InitializeComponent();
        Title = "No name";
    }

    public WidgetPanelControl(WidgetPanelControlParams data)
    {
        InitializeComponent();
        Title = data.Title;
        ActivateWidgetButton.Click += (sender, e) => data.OnActivate(sender, e);
    }
}