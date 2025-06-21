using Avalonia.Controls;
using Avalonia.Interactivity;

namespace widgets;

public partial class MainWindow : Window
{
    private readonly TimerManager _timerManager = new();

    public MainWindow()
    {
        InitializeComponent();

        _timerManager.OnTimerUpdateEvent += UpdateTimer;
        _timerManager.OnTimerStartEvent += () =>
        {
            PauseTimerButton.IsVisible = true;
            StartTimerButton.Content = "Reset Timer";
        };
        _timerManager.OnTimerStopEvent += () =>
        {
            PauseTimerButton.IsVisible = false;
            StartTimerButton.Content = "Start Timer";
            PauseTimerButton.Content = "Pause Timer";
        };
        _timerManager.OnTimerPause += () =>
        {
            PauseTimerButton.Content = "Resume Timer";
        };
        _timerManager.OnTimerResume += () =>
        {
            PauseTimerButton.Content = "Pause Timer";
        };
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