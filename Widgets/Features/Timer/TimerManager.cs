using System;
using Avalonia.Threading;

namespace widgets.Features.Timer;

public record TimerData(
    DispatcherTimer Dispatcher,
    DateTime TimerStart
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
    private DateTime? _pausedAt;
    public TimeSpan TimePaused { get; private set; }
    public bool IsPaused => _pausedAt.HasValue;

    public void Start()
    {
        _pausedAt ??= DateTime.UtcNow;
    }

    public void Finish()
    {
        TimePaused = GetTimePaused();
        _pausedAt = null;
    }

    public void Reset()
    {
        TimePaused = TimeSpan.Zero;
        _pausedAt = null;
    }

    private TimeSpan GetTimePaused()
    {
        if (!_pausedAt.HasValue)
            return TimeSpan.Zero;

        var currentTimePaused = DateTime.UtcNow - _pausedAt.Value;
        var totalTimePaused = TimePaused + currentTimePaused;

        return totalTimePaused;
    }
}

public delegate void OnTimerUpdate(TimerTime Time);
public delegate void OnTimerStart();
public delegate void OnTimerStop();
public delegate void OnPause();
public delegate void OnResume();

public class TimerManagerEvents
{
    public OnTimerUpdate? OnTimerUpdateEvent;
    public OnTimerStart? OnTimerStartEvent;
    public OnTimerStop? OnTimerStopEvent;
    public OnPause? OnTimerPauseEvent;
    public OnResume? OnTimerResumeEvent;
}

public class TimerManager
{
    private TimerData? _data;
    private readonly TimerPause _pause = new();
    public TimerManagerEvents? Events = null;

    public bool IsActive => _data is not null;
    public bool IsPaused => _pause.IsPaused;

    public void Start()
    {
        if (_data is not null || _pause.IsPaused)
            return;

        _data = new TimerData(
            new DispatcherTimer(TimeSpan.FromMilliseconds(500), DispatcherPriority.Background, (s, e) => HandleTimerUpdate()),
            DateTime.UtcNow.AddMilliseconds(-850)
        );

        _data.Dispatcher.Start();

        Events?.OnTimerStartEvent?.Invoke();

        HandleTimerUpdate();
    }

    public void Reset()
    {
        if (_data is null)
            return;

        _data.Dispatcher.Stop();
        _data = null;
        _pause.Reset();

        Events?.OnTimerStopEvent?.Invoke();
        HandleTimerUpdate();
    }

    public void Resume()
    {
        if (_data is null)
            return;

        _data.Dispatcher.Start();
        _pause.Finish();

        Events?.OnTimerResumeEvent?.Invoke();
        HandleTimerUpdate();
    }

    public void Pause()
    {
        if (_data is null)
            return;

        _pause.Start();
        _data.Dispatcher.Stop();

        Events?.OnTimerPauseEvent?.Invoke();
    }

    private void HandleTimerUpdate()
    {
        if (Events?.OnTimerUpdateEvent is null)
            return;

        var timerTime = new TimerTime(TimeSpan.Zero);

        if (_data is not null)
        {
            var now = DateTime.UtcNow;
            timerTime = new TimerTime(now - _data.TimerStart - _pause.TimePaused);
        }

        Events?.OnTimerUpdateEvent.Invoke(timerTime);
    }
}