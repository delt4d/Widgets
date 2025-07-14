using Avalonia.Controls;
using System.Threading;
using System.Threading.Tasks;
using WebViewControl;
using Widgets.UI.ViewModels;
using Widgets.UI.Views;

namespace Widgets.Features.Widget;

[WidgetLauncher("Webserver")]
public class WebserverWidgetLauncher : WidgetLauncherBase
{
    public WidgetWindow? Window { get; private set; }

    [WidgetArgs("Url")]
    public string Url { get; set; } = "https://google.com";

    [WidgetArgs("ReuseWindow")]
    public bool ReuseWindow { get; set; } = false;

    public override Task ExecuteAsync(CancellationToken? cancellationToken = null)
    {
        var WebViewComponent = new WebView
        {
            Address = Url,
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch
        };

        WebViewComponent.SetValue(Grid.RowProperty, 0);
        WebViewComponent.SetValue(Grid.ColumnProperty, 0);

        var widgetWindow = GetWidgetWindow();
        var mainGrid = widgetWindow.FindControl<Grid>("MainGrid");
        mainGrid?.Children.Add(WebViewComponent);

        widgetWindow.Show();

        return Task.CompletedTask;
    }

    private WidgetWindow GetWidgetWindow()
    {
        if (Window is not null && ReuseWindow)
            return Window;

        Window = new WidgetWindow(new WidgetViewModel(GetData()));
        Window.Closed += (_, _) => Window = null;

        return Window;
    }
}