using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Widgets.Controls.ViewModels;
using Widgets.Features;
using Widgets.Screens.ViewModels;
using Widgets.Screens.Views;

namespace Widgets.Helpers;

internal static class SampleWidgetsHelper
{
    public static List<WidgetItemViewModel> GetSampleWidgets()
    {
        var widget1 = new WidgetItemViewModel(new WebServerLauncher
        {
            WidgetWindow = GetWidgetWindow,
            Title = "Webview Widget 1",
            WebServerUrl = "http://localhost:5500/"
        });

        WidgetWindow? widgetWindow = null;
        var widget2 = new WidgetItemViewModel(new WebServerLauncher
        {
            WidgetWindow = () =>
            {
                widgetWindow ??= GetWidgetWindow();
                widgetWindow.Closed += (s, e) => widgetWindow = null;
                return widgetWindow;
            },
            Title = "Webview Widget 2 | SW",
            WebServerUrl = "http://localhost:5500/"
        });

        var widget3 = new WidgetItemViewModel(new LocalWidgetLauncher
        {
            TimerWidgetWindow = () => new TimerWidgetWindow(new TimerWidgetWindowViewModel()),
            Title = "Local Timer Widget"
        });

        return [widget1, widget2, widget3];
    }

    private static WidgetWindow GetWidgetWindow()
    {
        return new WidgetWindow()
        {
            ShowInTaskbar = false
        };
    }
}
