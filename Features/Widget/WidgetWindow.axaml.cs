using Avalonia.Controls;

namespace widgets;

public partial class WidgetWindow : Window
{
    public WidgetWindow()
    {
        InitializeComponent();
        this.ApplyDefaultWindowProperties(false);
        this.ApplyWidgetDefaultProperties();
    }
}