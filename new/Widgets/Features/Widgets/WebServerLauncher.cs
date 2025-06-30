using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using WebViewControl;

namespace Widgets.Features;

public class WebServerLauncher : BaseWidgetLauncher
{
    public required string WebServerUrl { get; set; }
    public required Window MainWindow { get; set; }
    public required Grid MainGrid { get; set; }
    public override string Type => "webserver";

    public override Task ExecuteAsync(CancellationToken? cancellationToken = null)
    {
        var WebViewComponent = new WebView
        {
            Address = WebServerUrl,
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch
        };
        
        WebViewComponent.SetValue(Grid.RowProperty, 0);
        WebViewComponent.SetValue(Grid.ColumnProperty, 0);

        MainGrid.Children.Add(WebViewComponent);
        MainWindow.Show();

        return Task.CompletedTask;
    }
}
