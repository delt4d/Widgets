using System.Collections.ObjectModel;
using Widgets.Controls.ViewModels;
using Widgets.ViewModels;

namespace Widgets.Screens.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<WidgetItemViewModel> Widgets { get; } = [];

    public MainWindowViewModel()
    {
        Widgets.Add(new WidgetItemViewModel
        {
            Title = "Widget 1"
        });

        Widgets.Add(new WidgetItemViewModel
        {
            Title = "Widget 2"
        });
    }
}
