using System;
using Avalonia.Threading;

namespace widgets;

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
    public bool IsPaused => _pausedAt is not null;

    public void Start()
    {
        _pausedAt ??= DateTime.UtcNow;
    }

    public void Reset()
    {
        _pausedAt = new DateTime();
        TimePaused = TimeSpan.Zero;
    }

    public void Finish()
    {
        TimePaused = GetTimePaused();
        _pausedAt = null;
    }

    private TimeSpan GetTimePaused()
    {
        if (_pausedAt is null)
            return TimeSpan.Zero;

        var currentTimePaused = DateTime.UtcNow - _pausedAt;
        var totalTimePaused = TimePaused + currentTimePaused;

        return (TimeSpan)totalTimePaused;
    }
}

public class TimerManager
{
    public delegate void OnTimerUpdate(TimerTime Time);
    public delegate void OnTimerStart();
    public delegate void OnTimerStop();
    public delegate void OnPause();
    public delegate void OnResume();

    public event OnTimerUpdate? OnTimerUpdateEvent;
    public event OnTimerStart? OnTimerStartEvent;
    public event OnTimerStop? OnTimerStopEvent;
    public event OnPause? OnTimerPause;
    public event OnResume? OnTimerResume;

    private TimerData? _data;
    private readonly TimerPause _pause = new();

    public bool IsActive => _data is not null;
    public bool IsPaused => _pause.IsPaused;

    public void Start()
    {
        if (_data is not null || _pause.IsPaused)
            return;

        _data = new TimerData(
            new DispatcherTimer(TimeSpan.FromMilliseconds(500), DispatcherPriority.Background, (s, e) => HandleTimerUpdate()),
            DateTime.UtcNow.AddSeconds(-1)
        );

        _data.Dispatcher.Start();
        OnTimerStartEvent?.Invoke();

        HandleTimerUpdate();
    }

    public void Resume()
    {
        if (_data is null)
            return;

        _data.Dispatcher.Start();
        _pause.Finish();
        OnTimerResume?.Invoke();
        HandleTimerUpdate();
    }

    public void Pause()
    {
        if (_data is null)
            return;
            
        _pause.Start();
        _data.Dispatcher.Stop();
        OnTimerPause?.Invoke();
    }

    public void Reset()
    {
        if (_data is null)
            return;

        _data.Dispatcher.Stop();
        _data = null;

        _pause.Reset();

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
            timerTime = new TimerTime(now - _data.TimerStart - _pause.TimePaused);
        }

        OnTimerUpdateEvent.Invoke(timerTime);
    }
}