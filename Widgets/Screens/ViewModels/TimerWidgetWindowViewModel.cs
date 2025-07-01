using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Widgets.Features;
using Widgets.ViewModels;

namespace Widgets.Screens.ViewModels;

public partial class TimerWidgetWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string formatedTime = "00:00:00";

    [ObservableProperty]
    private string startButtonText = "Start Timer";

    [ObservableProperty]
    private bool isPauseVisible = false;

    [ObservableProperty]
    private string pauseButtonText = "Pause Timer";

    private readonly TimerManager _timerManager = new();

    public TimerWidgetWindowViewModel()
    {
        _timerManager.Events = GetEvents();
    }

    [RelayCommand]
    private void StartTimer()
    {
        if (_timerManager.IsActive)
            _timerManager.Reset();
        else
            _timerManager.Start();
    }

    [RelayCommand]
    private void PauseTimer()
    {
        if (!_timerManager.IsActive)
            return;

        if (_timerManager.IsPaused)
            _timerManager.Resume();
        else
            _timerManager.Pause();
    }

    private void UpdateTimer(Timer time)
    {
        FormatedTime = time.Formatted;
    }
    
    private TimerEvents GetEvents()
    {
        return new TimerEvents
        {
            OnTimerStartEvent = () =>
            {
                IsPauseVisible = true;
                StartButtonText = "Reset Timer";
            },
            OnTimerStopEvent = () =>
            {
                IsPauseVisible = false;
                StartButtonText = "Start Timer";
                PauseButtonText = "Pause Timer";
            },
            OnTimerPauseEvent = () => PauseButtonText = "Resume Timer",
            OnTimerResumeEvent = () => PauseButtonText = "Pause Timer",
            OnTimerUpdateEvent = UpdateTimer
        };
    }
}
