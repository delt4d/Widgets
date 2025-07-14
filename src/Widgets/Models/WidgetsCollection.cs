using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Widgets.Features.Widget;
using Widgets.Helpers;
using Widgets.UI.ViewModels;
using Widgets.Utils;

namespace Widgets.Models;

public class WidgetsCollection : ObservableCollection<WidgetItemViewModel>
{
    public static readonly IEnumerable<WidgetItemViewModel> SampleWidgets;

    static WidgetsCollection()
    {
#if DEBUG
        SampleWidgets = SampleWidgetsHelper.GetSampleWidgets();
#endif
    }

    public IEnumerable<IWidgetLauncher> Launchers =>
        Items.Where(x => !SampleWidgets.Contains(x))
            .Select(x => x.WidgetLauncher);

    public WidgetsCollection()
    {
        CollectionChanged += OnCollectionChanged;
    }

    public async Task LoadWidgets()
    {
        var items = new List<WidgetItemViewModel>();

        await foreach (var launcher in LauncherStorage.LoadAsync())
        {
            items.Add(CreateWidget(launcher));
        }

        foreach (var vm in SampleWidgets)
        {
            items.Add(vm);
        }

        Clear();
        AddRange(items);
    }

    public void AddRange(IEnumerable<WidgetItemViewModel> items)
    {
        foreach (var item in items)
        {
            Add(item);
        }
    }

    public void CreateNewWidget()
    {
        var launcher = new TimerWidgetLauncher
        {
            Title = $"Created Widget {Count - SampleWidgets.Count() + 1}"
        };
        var vm = CreateWidget(launcher);
        Insert(0, vm);
    }

    private WidgetItemViewModel CreateWidget(IWidgetLauncher launcher)
    {
        var vm = new WidgetItemViewModel(launcher);
        vm.DeleteEvent += (s, e) => Remove(vm);
        return vm;
    }
    private async void OnCollectionChanged(
        object? sender,
        NotifyCollectionChangedEventArgs e)
    {
        try
        {   
            await LauncherStorage.SaveAsync(Launchers);
        } catch(Exception) {}
    }
}
