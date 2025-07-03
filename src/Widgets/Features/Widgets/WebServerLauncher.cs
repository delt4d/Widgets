using Avalonia.Controls;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebViewControl;
using Widgets.Screens.Views;

namespace Widgets.Features;

public class WebServerLauncher : BaseWidgetLauncher
{
    [JsonProperty]
    public required string WebServerUrl { get; set; }
    
    [JsonProperty]
    public override string Type => "webserver";

    public required Func<Window>? WidgetWindow { get; set; }

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

        var widgetWindow = WidgetWindow?.Invoke() ?? new WidgetWindow();
        var mainGrid = widgetWindow.FindControl<Grid>("MainGrid");
        mainGrid?.Children.Add(WebViewComponent);
        widgetWindow.Show();

        return Task.CompletedTask;
    }
}
