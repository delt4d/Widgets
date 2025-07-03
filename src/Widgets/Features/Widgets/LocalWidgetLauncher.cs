using Avalonia.Controls;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using Widgets.Screens.Views;

namespace Widgets.Features;

public class LocalWidgetLauncher : BaseWidgetLauncher
{
    [JsonProperty]
    public override string Type => "local";
    public required Func<Window>? TimerWidgetWindow { get; set; }

    public override Task ExecuteAsync(CancellationToken? cancellationToken = null)
    {
        var window = TimerWidgetWindow?.Invoke() ?? new TimerWidgetWindow();
        window.Show();
        return Task.CompletedTask;
    }
}
