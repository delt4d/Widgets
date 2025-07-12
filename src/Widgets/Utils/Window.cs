using System;

namespace Widgets.Utils;

public class Window<TViewModel> : Avalonia.Controls.Window
{
    public EventHandler? ViewModelLoaded;
    public bool IsViewModelLoaded { get; private set; } = false;
    public TViewModel ViewModel { get; private set; } = default!;

    public Window()
    {
        DataContextChanged += OnDataContextChanged;
    }

    public Window(TViewModel viewModel)
    {
        DataContextChanged += OnDataContextChanged;
        DataContext = viewModel;
    }

    public virtual void OnViewModelLoaded()
    {
    }

    private void OnDataContextChanged(object? sender, EventArgs e)
    {
        if (DataContext is not TViewModel vm)
        {
            ViewModel = default!;
            return;
        }

        ViewModel = vm;
        IsViewModelLoaded = true;
        OnViewModelLoaded();
        ViewModelLoaded?.Invoke(this, EventArgs.Empty);
    }
}
