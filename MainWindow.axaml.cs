using Avalonia.Controls;
using Avalonia.Interactivity;

namespace widgets;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.ApplyDefaultWindowProperties();
    }

    public void CreateNewWindowHandler(object? sender, RoutedEventArgs args)
    {
        var window = new WidgetWindow("https://github.com/delt4d/")
        {
            ShowInTaskbar = false
        };
        window.Show(this);
    }
}