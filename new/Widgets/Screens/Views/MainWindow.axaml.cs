using Avalonia.Controls;
using Widgets.Utils;

namespace Widgets.Screens.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Initialize();
    }

    private void Initialize()
    {
        this.ApplyWindowProperties(new WindowProperties
        {
            ExtendClientArea = true,
            SystemDecorations = SystemDecorations.BorderOnly
        });
    }
}