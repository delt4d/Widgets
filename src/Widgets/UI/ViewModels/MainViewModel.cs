using CommunityToolkit.Mvvm.Input;
using Widgets.Models;

namespace Widgets.UI.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public WidgetsCollection Widgets { get; } = [];

    public MainViewModel()
    {
        Initialize();
    }

    private async void Initialize()
    {
        await Widgets.LoadWidgets();
    }

    [RelayCommand]
    private void CreateNewWidget() => Widgets.CreateNewWidget();
}
