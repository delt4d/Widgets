namespace Widgets.Features.TimerManager;

public class TimerEvents
{
    public OnTimerUpdate? OnTimerUpdateEvent { get; set; }
    public OnTimerStart? OnTimerStartEvent { get; set; }
    public OnTimerStop? OnTimerStopEvent { get; set; }
    public OnPause? OnTimerPauseEvent { get; set; }
    public OnResume? OnTimerResumeEvent { get; set; }
}

public delegate void OnTimerUpdate(Timer Time);
public delegate void OnTimerStart();
public delegate void OnTimerStop();
public delegate void OnPause();
public delegate void OnResume();