using Widgets.Features;
using Widgets.ViewModels;

namespace Widgets.Controls.ViewModels;

public class WidgetItemViewModel : ViewModelBase
{
    public string Title => WidgetLauncher.Title;
    public BaseWidgetLauncher WidgetLauncher { get; set; }
    public WidgetItemViewModel()
    {
        WidgetLauncher = VoidWidgetLauncher.Default;
    }
    public WidgetItemViewModel(BaseWidgetLauncher launcher)
    {
        WidgetLauncher = launcher;
    }
}