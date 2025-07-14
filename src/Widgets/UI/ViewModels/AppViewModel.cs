using CommunityToolkit.Mvvm.Input;
using System;

namespace Widgets.UI.ViewModels;

public partial class AppViewModel : ViewModelBase
{
    public EventHandler? Exit { get; set; }
    public EventHandler? ShowMainWindow;

    [RelayCommand]
    public void ShowMainWindowCommand()
    {
        ShowMainWindow?.Invoke(this, EventArgs.Empty);
    }

    [RelayCommand]
    public void ExitCommand()
    {
        Exit?.Invoke(this, EventArgs.Empty);
    }
}

