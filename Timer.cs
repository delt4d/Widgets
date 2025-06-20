using System;
using Avalonia.Threading;

namespace widgets;

public record TimerData(
    DispatcherTimer Dispatcher,
    DateTime TimerStart,
    TimeSpan TimePaused
);

public record TimerTime(TimeSpan Time)
{
    public string Hours => Time.Hours.ToString("D2");
    public string Minutes => Time.Minutes.ToString("D2");
    public string Seconds => Time.Seconds.ToString("D2");
    public string Formatted => $"{Hours}:{Minutes}:{Seconds}";
}

public class TimerPause
{
    public DateTime PausedAt { get; } = DateTime.UtcNow;
    public TimeSpan TimePaused => DateTime.UtcNow - PausedAt;
}

public class TimerManager
{
    public delegate void OnTimerUpdate(TimerTime Time);
    public delegate void OnTimerStart();
    public delegate void OnTimerStop();

    public event OnTimerUpdate? OnTimerUpdateEvent;
    public event OnTimerStart? OnTimerStartEvent;
    public event OnTimerStop? OnTimerStopEvent;

    private TimerData? _data;

    public bool IsActive => _data is not null;

    public void Start()
    {
        if (_data is not null)
            Stop();

        _data = new TimerData(
            new DispatcherTimer(TimeSpan.FromMilliseconds(500), DispatcherPriority.Background, (s, e) => HandleTimerUpdate()),
            DateTime.UtcNow.AddSeconds(-1),
            TimeSpan.Zero
        );

        _data.Dispatcher.Start();
        OnTimerStartEvent?.Invoke();

        HandleTimerUpdate();
    }

    public void Stop()
    {
        if (_data is null)
            return;

        _data.Dispatcher.Stop();
        _data = null;
        OnTimerStopEvent?.Invoke();

        HandleTimerUpdate();
    }

    private void HandleTimerUpdate()
    {
        if (OnTimerUpdateEvent is null)
            return;

        var timerTime = new TimerTime(TimeSpan.Zero);

        if (_data is not null)
        {
            var now = DateTime.UtcNow;
            timerTime = new TimerTime(now - _data.TimerStart);
        }

        OnTimerUpdateEvent.Invoke(timerTime);
    }
}