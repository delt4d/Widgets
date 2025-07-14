using System.Collections.Generic;
using Widgets.UI.ViewModels;
using Widgets.Features.Widget;

namespace Widgets.Helpers;

internal static class SampleWidgetsHelper
{
    public static List<WidgetItemViewModel> GetSampleWidgets()
    {
        var widget1 = new WidgetItemViewModel(new WebserverWidgetLauncher
        {
            Title = "Webview Widget",
            Url = "http://localhost:5500/"
        });

        var widget2 = new WidgetItemViewModel(new WebserverWidgetLauncher
        {
            ReuseWindow = true,
            Url = "http://localhost:5500/",
            Title = "Webview Widget | Same Window"
        });

        var widget3 = new WidgetItemViewModel(new TimerWidgetLauncher
        {
            Title = "Local Timer Widget"
        });

        var widget4 = new WidgetItemViewModel(new TimerWidgetLauncher
        {
            ReuseWindow = true,
            Title = "Local Timer Widget | Same Window"
        });

        return [widget1, widget2, widget3, widget4];
    }
}
