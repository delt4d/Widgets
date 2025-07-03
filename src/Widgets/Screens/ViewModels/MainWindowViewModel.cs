using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Widgets.Controls.ViewModels;
using Widgets.Features;
using Widgets.Screens.Views;
using Widgets.Utils;
using Widgets.ViewModels;

namespace Widgets.Screens.ViewModels;

public class WidgetObservableCollection : ObservableCollection<WidgetItemViewModel>
{
    public IEnumerable<BaseWidgetLauncher> GetItemsWidgetLauncher()
    {
        foreach (var item in Items)
        {
            yield return item.WidgetLauncher;
        }
    }
}

public partial class MainWindowViewModel : ViewModelBase
{
    public WidgetObservableCollection Widgets { get; } = [];
    private int CreatedWidgetsCount => Widgets.Count;

    public MainWindowViewModel()
    {
        Widgets.CollectionChanged += OnCollectionChanged;
        LoadWidgets();
    }

    private async void OnCollectionChanged(object? sender, EventArgs e)
    {
        await LauncherStorage.SaveAsync(Widgets.GetItemsWidgetLauncher());
    }

    private async void LoadWidgets()
    {
        foreach (var item in await LauncherStorage.LoadAsync())
        {
            Widgets.Add(new WidgetItemViewModel(item));
        }
    }

    [RelayCommand]
    private void CreateNewWidget()
    {
        Widgets.Insert(0, new WidgetItemViewModel(new LocalWidgetLauncher
        {
            TimerWidgetWindow = () => new TimerWidgetWindow(new TimerWidgetWindowViewModel()),
            Title = $"Created Widget {CreatedWidgetsCount+1}"
        }));
    }
}
