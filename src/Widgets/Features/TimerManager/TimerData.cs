using System;
using Avalonia.Threading;

namespace Widgets.Features.TimerManager;

public class TimerData
{
    private readonly DispatcherTimer _dispatcher;
    private readonly Action _onTimerUpdate;
    
    public DateTime StartedAt { get; private set; }

    public TimerData(Action onTimerUpdate)
    {
        _onTimerUpdate = onTimerUpdate;
        _dispatcher = new DispatcherTimer(
            TimeSpan.FromMilliseconds(500), 
            DispatcherPriority.Background, (s, e) => HandleTimerUpdate());

        StartedAt = DateTime.UtcNow.AddMilliseconds(-850);
    }

    public void Start() => _dispatcher.Start();
    public void Stop() => _dispatcher.Stop();
    private void HandleTimerUpdate() => _onTimerUpdate.Invoke();
}