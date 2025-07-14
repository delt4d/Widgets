using System;
using Widgets.Features.Widget;

namespace Widgets.UI.ViewModels;

public class WidgetItemViewModel : ViewModelBase
{
    public string Title => WidgetLauncher.Title;
    public IWidgetLauncher WidgetLauncher { get; set; }
    public EventHandler? DeleteEvent;
    public WidgetItemViewModel()
    {
        WidgetLauncher = WidgetLauncherBase.Default;
    }
    public WidgetItemViewModel(IWidgetLauncher launcher)
    {
        WidgetLauncher = launcher;
    }
}