using System.Collections.ObjectModel;
using Widgets.Controls.ViewModels;
using Widgets.ViewModels;

namespace Widgets.Screens.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<WidgetItemViewModel> Widgets { get; } = [];
}
