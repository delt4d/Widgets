using Avalonia.Controls;
using Widgets.UI.ViewModels;
using Widgets.Utils;

namespace Widgets.UI.Views;

public partial class TimerWidgetWindow : Window<TimerWidgetViewModel>
{
    public TimerWidgetWindow() : base(new TimerWidgetViewModel())
    {
        InitializeComponent();
        Initialize();
    }

    public TimerWidgetWindow(TimerWidgetViewModel vm) : base(vm)
    {
        InitializeComponent();
        Initialize();
    }

    private void Initialize()
    {
        this.ApplyWindowProperties(new WindowProperties
        {
            ExtendClientArea = true,
            WindowStyles = WindowStyles.Acrylic,
            WindowSizes = WindowSizes.Widget
        });
    }
}