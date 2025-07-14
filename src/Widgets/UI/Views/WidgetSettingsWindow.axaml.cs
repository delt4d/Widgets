using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Widgets.UI.ViewModels;
using Widgets.Utils;

namespace Widgets.UI.Views;

public partial class WidgetSettingsWindow : Window<WidgetSettingsViewModel>
{
    public WidgetSettingsWindow() : base(new WidgetSettingsViewModel())
    {
        InitializeComponent();
        Initialize();
    }

    public WidgetSettingsWindow(WidgetSettingsViewModel vm) : base(vm)
    {
        InitializeComponent();
        Initialize();
    }

    private void Initialize()
    {
        this.ApplyWindowProperties(new WindowProperties
        {
            ExtendClientArea = true,
            SystemDecorations = SystemDecorations.Full,
            WindowStyles = WindowStyles.Solid
        });
        BorderThickness = new Thickness(10);
        BorderBrush = Brushes.AliceBlue;
        Padding = new Thickness(0, 25, 0, 0);
    }
}