using Avalonia.Controls;
using WebViewControl;
using Widgets.UI.ViewModels;
using Widgets.Utils;

namespace Widgets.UI.Views;

public partial class WidgetWindow : Window<WidgetViewModel>
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