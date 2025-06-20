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
        };
    }

    public void StartTimer(object sender, RoutedEventArgs e)
    {
        if (_timerManager.IsActive)
            _timerManager.Stop();
        else
            _timerManager.Start();
    }

    public void UpdateTimer(TimerTime time)
    {
        Timer.Text = time.Formatted;
    }
}