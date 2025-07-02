using Widgets.Screens.ViewModels;
using Widgets.Utils;

namespace Widgets.Screens.Views;

public partial class TimerWidgetWindow : Window<TimerWidgetWindowViewModel>
{
    public TimerWidgetWindow()
    {
        InitializeComponent();
        Initialize();
    }

    public TimerWidgetWindow(TimerWidgetWindowViewModel vm) : base(vm)
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