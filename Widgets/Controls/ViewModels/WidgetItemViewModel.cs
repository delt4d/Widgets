using Widgets.Features;
using Widgets.ViewModels;

namespace Widgets.Controls.ViewModels;

public class WidgetItemViewModel(BaseWidgetLauncher launcher) : ViewModelBase
{
    public string Title => WidgetLauncher.Title;
    public BaseWidgetLauncher WidgetLauncher { get; set; } = launcher;
}