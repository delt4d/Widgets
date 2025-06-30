using Avalonia.Controls;
using System;
using WebViewControl;
using Widgets.Screens.ViewModels;
using Widgets.Utils;

namespace Widgets.Screens.Views;

public partial class WidgetWindow : Window
{
    public WebView? WebViewComponent { get; set; }

    public WidgetWindow()
    {
        InitializeComponent();

        this.ApplyWindowProperties(new WindowProperties
        {
            ExtendClientArea = false,
            SystemDecorations = SystemDecorations.Full,
            WindowStyles = WindowStyles.Acrylic,
            WindowSizes = WindowSizes.Widget
        });

        Initialize();
    }

    private void Initialize()
    {
        if (DataContext is WidgetWindowViewModel vm)
        {
            vm.PropertyChanged += (s, e) => LoadWebview(vm.AddressUrl);
        }

        throw new Exception($"Data Context needs to be {nameof(WidgetWindowViewModel)}");
    }

    private void LoadWebview(string addressUrl)
    {
        if (WebViewComponent is null)
            MainGrid.Children.Clear();

        WebViewComponent = new WebView
        {
            Address = addressUrl,
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch
        };
        WebViewComponent.SetValue(Grid.RowProperty, 0);
        WebViewComponent.SetValue(Grid.ColumnProperty, 0);

        MainGrid.Children.Add(WebViewComponent);
    }
}