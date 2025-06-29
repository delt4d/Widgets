using Avalonia.Threading;
using System;

namespace Widgets.Features;

public struct Timer(TimeSpan Time)
{
    public readonly string Hours => Time.Hours.ToString("D2");
    public readonly string Minutes => Time.Minutes.ToString("D2");
    public readonly string Seconds => Time.Seconds.ToString("D2");
    public readonly string Formatted => $"{Hours}:{Minutes}:{Seconds}";
}

public class TimerPause
{
    private DateTime? _pausedAt;

    public TimeSpan TimePaused { get; private set; }
    public bool IsPaused => _pausedAt.HasValue;

    public void Start() => _pausedAt ??= DateTime.UtcNow;
    public void Finish()
    {
        TimePaused = GetTimePaused(_pausedAt, TimePaused);
        _pausedAt = null;
    }
    public void Reset()
    {
        TimePaused = TimeSpan.Zero;
        _pausedAt = null;
    }

    private static TimeSpan GetTimePaused(DateTime? pausedAt, TimeSpan timePaused)
    {
        if (!pausedAt.HasValue) return TimeSpan.Zero;
        var currentTimePaused = DateTime.UtcNow - pausedAt.Value;
        var totalTimePaused = timePaused + currentTimePaused;
        return totalTimePaused;
    }
}

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

public class TimerManager
{
    private TimerData? _data;
    private readonly TimerPause _pause = new();

    public TimerManagerEvents Events = new();
    public bool IsActive => _data is not null;
    public bool IsPaused => _pause.IsPaused;

    public void Start()
    {
        if (_data is not null || _pause.IsPaused)
            return;

        _data = new TimerData(HandleTimerUpdate);
        _data.Start();

        Events.OnTimerStartEvent?.Invoke();

        HandleTimerUpdate();
    }

    public void Reset()
    {
        if (_data is null)
            return;

        _data.Stop();
        _data = null;

        _pause.Reset();

        Events.OnTimerStopEvent?.Invoke();
        HandleTimerUpdate();
    }

    public void Resume()
    {
        if (_data is null)
            return;

        _data.Start();
        _pause.Finish();

        Events.OnTimerResumeEvent?.Invoke();
        HandleTimerUpdate();
    }

    public void Pause()
    {
        if (_data is null)
            return;

        _pause.Start();
        _data.Stop();

        Events.OnTimerPauseEvent?.Invoke();
    }

    private void HandleTimerUpdate()
    {
        if (Events.OnTimerUpdateEvent is null)
            return;

        var timer = new Timer(TimeSpan.Zero);

        if (_data is not null)
        {
            var now = DateTime.UtcNow;
            timer = new Timer(now - _data.StartedAt - _pause.TimePaused);
        }

        Events.OnTimerUpdateEvent.Invoke(timer);
    }
}