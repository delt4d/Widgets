using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Widgets.UI.Views;
using Widgets.Features.Widget;
using Widgets.Helpers;
using Widgets.Utils;

namespace Widgets.UI.ViewModels;

public class WidgetObservableCollection : ObservableCollection<WidgetItemViewModel>
{
    public IEnumerable<IWidgetLauncher> GetItemsToSave() => Items
        .Where(x => x.ShouldPersist)
        .Select(x => x.WidgetLauncher);
}

public partial class MainViewModel : ViewModelBase
{
    public WidgetObservableCollection Widgets { get; } = [];

    public MainViewModel()
    {
        Widgets.CollectionChanged += OnCollectionChanged;
        LoadWidgets();
    }

    private async void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        await LauncherStorage.SaveAsync(Widgets.GetItemsToSave());
    }

    private async void LoadWidgets()
    {
        await foreach (var item in LauncherStorage.LoadAsync())
            AddWidget(item);

#if DEBUG
        foreach (var vm in SampleWidgetsHelper.GetSampleWidgets())
        {
            vm.ShouldPersist = false;
            AddWidget(vm);
        }
#endif
    }

    [RelayCommand]
    private void CreateNewWidget()
    {
        AddWidget(new TimerWidgetLauncher
        {
            Title = $"Created Widget {Widgets.Count + 1}"
        }, true);
    }

    private void AddWidget(IWidgetLauncher launcher, bool insertAtStart = false)
    {
        var vm = new WidgetItemViewModel(launcher);
        vm.OnRemoveRequested += (s, e) =>
        {
            var view = (WidgetItemControl)s!;
            Widgets.Remove(view.ViewModel);
        };
        AddWidget(vm, insertAtStart);
    }
    
    private void AddWidget(WidgetItemViewModel vm, bool insertAtStart = false)
    {
        if (insertAtStart)
            Widgets.Insert(0, vm);
        else
            Widgets.Add(vm);
    }
}
