using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace Widgets.Features;

public class LocalWidgetLauncher : BaseWidgetLauncher
{
    public override string Type => "local";
    public required Func<Window> TimerWidgetWindow { get; set; }

    public override Task ExecuteAsync(CancellationToken? cancellationToken = null)
    {
        var window = TimerWidgetWindow();
        window.Show();
        return Task.CompletedTask;
    }
}
