using Avalonia;
using Avalonia.Media.Fonts;
using System;
using System.IO;
using Widgets.Views;

namespace Widgets.Desktop;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        FixCurrentWorkingDictionary();
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    private static void FixCurrentWorkingDictionary()
    {
        if (Path.GetDirectoryName(Environment.ProcessPath) is { } dir)
            Environment.CurrentDirectory = dir;
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .ConfigureFonts(manager =>
            {
                manager.AddFontCollection(
                    new EmbeddedFontCollection(new Uri("fonts:App", UriKind.Absolute),
                    new Uri("avares://Widgets/Resources")));
            });
}
