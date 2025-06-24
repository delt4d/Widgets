using Avalonia.Controls;
using WebViewControl;

namespace widgets;

public partial class WidgetWindow : Window
{
    public WidgetWindow(string url)
    {
        InitializeComponent();
        this.ApplyDefaultWindowProperties(false);
        this.ApplyWidgetDefaultProperties();

        var webView = new WebView
        {
            Address = url,
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch
        };

        Content = webView;
    }
}