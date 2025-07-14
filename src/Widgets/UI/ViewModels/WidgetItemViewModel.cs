using System;
using Widgets.Features.Widget;

namespace Widgets.UI.ViewModels;

public class WidgetItemViewModel : ViewModelBase
{
    public string Title => WidgetLauncher.Title;
    public bool ShouldPersist { get; set; } = true;
    public IWidgetLauncher WidgetLauncher { get; set; }
    public EventHandler? OnRemoveRequested;
    public WidgetItemViewModel()
    {
        WidgetLauncher = WidgetLauncherBase.Default;
    }
    public WidgetItemViewModel(IWidgetLauncher launcher)
    {
        WidgetLauncher = launcher;
    }
}