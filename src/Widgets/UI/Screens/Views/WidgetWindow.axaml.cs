using Avalonia.Controls;
using WebViewControl;
using Widgets.Screens.ViewModels;
using Widgets.Utils;

namespace Widgets.Screens.Views;

public partial class WidgetWindow : Window<WidgetWindowViewModel>
{
    public WebView? WebViewComponent { get; set; }

    public WidgetWindow()
    {
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