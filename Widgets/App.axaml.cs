using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using widgets.Features.Main;

namespace widgets;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
            // desktop.MainWindow.Opened += (sender, e) =>
            // {
            //     var screenSize = desktop.MainWindow.Screens.Primary!.WorkingArea.Size;
            //     var windowSize = PixelSize.FromSize(desktop.MainWindow.ClientSize, desktop.MainWindow.Screens.Primary.Scaling);
            //     desktop.MainWindow.Position = new PixelPoint(
            //         screenSize.Width - windowSize.Width,
            //         screenSize.Height - windowSize.Height);
            // };
        }

        base.OnFrameworkInitializationCompleted();
    }
}