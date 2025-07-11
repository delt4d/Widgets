using System.Threading;
using System.Threading.Tasks;
using Widgets.Features.Attributes;
using Widgets.Screens.Views;

namespace Widgets.Features;

[WidgetLauncher("TimerWidget")]
public class TimerWidgetLauncher : WidgetLauncherBase
{
    public TimerWidgetWindow? Window { get; private set; }

    [WidgetAdditional("ReuseWindow")]
    public bool ReuseWindow { get; set; } = false;

    public override Task ExecuteAsync(CancellationToken? cancellationToken = null)
    {
        var window = GetWidgetWindow();
        window.Show();
        return Task.CompletedTask;
    }

    private TimerWidgetWindow GetWidgetWindow()
    {
        if (Window is not null && ReuseWindow)
            return Window;

        Window = new TimerWidgetWindow();
        Window.Closed += (_, _) => Window = null;

        return Window;
    }
}