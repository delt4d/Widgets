using System;

namespace Widgets.Features.TimerManager;

public class TimerManager
{
    private TimerData? _data;
    private readonly TimerPause _pause = new();

    public TimerEvents Events = new();
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