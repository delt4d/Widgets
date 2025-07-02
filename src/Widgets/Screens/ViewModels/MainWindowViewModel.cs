using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using Widgets.Controls.ViewModels;
using Widgets.Features;
using Widgets.Screens.Views;
using Widgets.ViewModels;

namespace Widgets.Screens.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<WidgetItemViewModel> Widgets { get; } = [];
    private int _createdWidgetsCount = 0;

    [RelayCommand]
    private void CreateNewWidget()
    {
        Widgets.Insert(0, new WidgetItemViewModel(new LocalWidgetLauncher
        {
            TimerWidgetWindow = () => new TimerWidgetWindow(new TimerWidgetWindowViewModel()),
            Title = $"Created Widget {++_createdWidgetsCount}"
        }));
    }
}
