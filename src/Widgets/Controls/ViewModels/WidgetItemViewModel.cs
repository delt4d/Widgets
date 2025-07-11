using Widgets.Features;
using Widgets.ViewModels;

namespace Widgets.Controls.ViewModels;

public class WidgetItemViewModel : ViewModelBase
{
    public string Title => WidgetLauncher.Title;
    public bool SaveToFile { get; set; } = true;
    public IWidgetLauncher WidgetLauncher { get; set; }
    public WidgetItemViewModel()
    {
        WidgetLauncher = WidgetLauncherBase.Default;
    }
    public WidgetItemViewModel(IWidgetLauncher launcher)
    {
        WidgetLauncher = launcher;
    }
}