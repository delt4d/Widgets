using Avalonia.Controls;
using Widgets.UI.ViewModels;
using Widgets.Utils;

namespace Widgets.UI.Views;

public partial class WidgetWindow : Window<WidgetViewModel>
{
    public WidgetWindow(WidgetViewModel vm) : base(vm)
    {
        Title = ViewModel.Title;
        InitializeComponent();
        Initialize();
    }

    public WidgetWindow() 
    {
        Title = "Widget Window";
        InitializeComponent();
        Initialize();
    }

    private void Initialize()
    {
        this.ApplyWindowProperties(new WindowProperties
        {
            ExtendClientArea = false,
            SystemDecorations = SystemDecorations.Full,
            WindowStyles = WindowStyles.Acrylic,
            WindowSizes = WindowSizes.Widget
        });
    }
}