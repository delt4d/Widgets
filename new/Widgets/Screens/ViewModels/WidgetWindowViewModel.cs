using CommunityToolkit.Mvvm.ComponentModel;
using Widgets.ViewModels;

namespace Widgets.Screens.ViewModels;

public partial class WidgetWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string addressUrl = "https://google.com/";
}