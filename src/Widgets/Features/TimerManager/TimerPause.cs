using System;

namespace Widgets.Features.TimerManager;

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
