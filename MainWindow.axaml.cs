using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;

namespace widgets;

public partial class MainWindow : Window
{
    public DateTime TimerStartedAt { get; set; } = DateTime.UtcNow;
    public DispatcherTimer? TimerDispatcher { get; set; }

    public MainWindow()
    {
        InitializeComponent();
    }

    public void StartTime(object sender, RoutedEventArgs e)
    {
        if (TimerDispatcher is null)
        {
            TimerDispatcher = new DispatcherTimer(
                TimeSpan.FromMilliseconds(500),
                DispatcherPriority.Background,
                (sender, e) => UpdateTimer());

            TimerStartedAt = DateTime.UtcNow;
            TimerDispatcher.Start();

            UpdateTimer();
        }
        else
        {
            TimerDispatcher.Stop();
            TimerDispatcher = null;
            Timer.Text = "00:00:00";
        }
    }

    public void UpdateTimer()
    {
        var now = DateTime.UtcNow;
        var time = now - TimerStartedAt;
        var hours = time.Hours.D2();
        var minutes = time.Minutes.D2();
        var seconds = time.Seconds.D2();
        Timer.Text = $"{hours}:{minutes}:{seconds}";
    }
}

public static class Extensions
{
    public static string D2(this int value)
    {
        return value.ToString("D2");
    }
}