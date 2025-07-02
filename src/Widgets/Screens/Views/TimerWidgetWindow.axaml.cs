using Avalonia.Controls;
using Widgets.Screens.ViewModels;
using Widgets.Utils;

namespace Widgets.Screens.Views;

public partial class TimerWidgetWindow : Window
{
    public TimerWidgetWindow()
    {
        InitializeComponent();
        Initialize();
    }

    public TimerWidgetWindow(TimerWidgetWindowViewModel vm)
    {
        InitializeComponent();
        Initialize();
        DataContext = vm;
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