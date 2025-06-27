using Avalonia.Controls;
using WebViewControl;

namespace widgets.Features.Widget.Windows;

public partial class WidgetWindow : Window
{
    public WebView? WebViewComponent;

    public WidgetWindow(string url)
    {
        InitializeComponent();
        Initialize(url);
    }

    public WidgetWindow()
    {
        InitializeComponent();
        Initialize(null);
    }

    private void Initialize(string? url)
    {
        url ??= "https://google.com/";

        WebViewComponent = new WebView
        {
            Address = url,
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch
        };

        MainGrid.Children.Add(WebViewComponent);

        WebViewComponent.SetValue(Grid.RowProperty, 0);
        WebViewComponent.SetValue(Grid.ColumnProperty, 0);

        this.ApplyWidgetDefaultProperties();
        this.ApplyDefaultWindowProperties();
    }
}