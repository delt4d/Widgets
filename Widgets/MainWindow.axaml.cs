using Avalonia.Controls;
using Avalonia.Interactivity;
using widgets.Features.Widget;

namespace widgets;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        this.ApplyDefaultWindowProperties(prop =>
        {
            prop.ExtendClientAreaToDecorationsHint = true;
            prop.Transparent = true;
        });
    }

    public void CreateNewWindowHandler(object? sender, RoutedEventArgs args)
    {
        var widgetWindow = new WidgetWindow("https://github.com/delt4d/")
        {
            ShowInTaskbar = false
        };
        widgetWindow.Show(this);
    }
}