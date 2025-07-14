using Widgets.UI.ViewModels;
using Widgets.Utils;

namespace Widgets.UI.Views;

public partial class TimerWidgetWindow : Window<TimerWidgetWindowViewModel>
{
    public TimerWidgetWindow() : base(new TimerWidgetWindowViewModel())
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