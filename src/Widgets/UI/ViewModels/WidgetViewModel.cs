using Widgets.Features.Widget;

namespace Widgets.UI.ViewModels;

public partial class WidgetViewModel(WidgetData data) : ViewModelBase
{
    public string Title => Data.Title;
    public WidgetData Data { get; set; } = data;
}