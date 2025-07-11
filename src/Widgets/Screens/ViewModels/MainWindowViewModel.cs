using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Widgets.Controls.ViewModels;
using Widgets.Features;
using Widgets.Helpers;
using Widgets.Utils;
using Widgets.ViewModels;

namespace Widgets.Screens.ViewModels;

public class WidgetObservableCollection : ObservableCollection<WidgetItemViewModel>
{
    public IEnumerable<IWidgetLauncher> GetItemsToSave() => Items
        .Where(x => x.SaveToFile)
        .Select(x => x.WidgetLauncher);
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
        await LauncherStorage.SaveAsync(Widgets.GetItemsToSave());
    }

    private async void LoadWidgets()
    {
        await foreach (var item in LauncherStorage.LoadAsync())
        {
            Widgets.Add(new WidgetItemViewModel(item));
        }

        foreach (var vm in SampleWidgetsHelper.GetSampleWidgets())
        {
            vm.SaveToFile = false;
            Widgets.Add(vm);
        }
    }

    [RelayCommand]
    private void CreateNewWidget()
    {
        Widgets.Insert(0, new WidgetItemViewModel(new TimerWidgetLauncher
        {
            Title = $"Created Widget {CreatedWidgetsCount+1}"
        }));
    }
}
