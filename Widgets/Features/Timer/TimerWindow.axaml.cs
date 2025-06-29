using Avalonia.Controls;
using Avalonia.Interactivity;

namespace widgets.Features.Timer;

public partial class TimerWindow : Window
{
    private readonly TimerManager _timerManager = new();

    public TimerWindow()
    {
        InitializeComponent();

        _timerManager.Events = new TimerManagerEvents
        {
            OnTimerStartEvent = () =>
            {
                PauseTimerButton.IsVisible = true;
                StartTimerButton.Content = "Reset Timer";
            },
            OnTimerStopEvent = () =>
            {
                PauseTimerButton.IsVisible = false;
                StartTimerButton.Content = "Start Timer";
                PauseTimerButton.Content = "Pause Timer";
            },
            OnTimerUpdateEvent = UpdateTimer,
            OnTimerPauseEvent = () => PauseTimerButton.Content = "Resume Timer",
            OnTimerResumeEvent = () => PauseTimerButton.Content = "Pause Timer"
        };

        this.ApplyDefaultWindowProperties(new()
        {
            ExtendClientAreaToDecorationsHint = true
        });
        this.ApplyWidgetDefaultProperties();
        this.ApplyDefaultAcrylicProperties();
    }

    public void StartTimer(object sender, RoutedEventArgs e)
    {
        if (_timerManager.IsActive)
            _timerManager.Reset();
        else
            _timerManager.Start();
    }

    public void PauseTimer(object sender, RoutedEventArgs e)
    {
        if (!_timerManager.IsActive)
            return;

        if (_timerManager.IsPaused)
            _timerManager.Resume();
        else
            _timerManager.Pause();
    }

    public void UpdateTimer(TimerTime time)
    {
        Timer.Text = time.Formatted;
    }
}