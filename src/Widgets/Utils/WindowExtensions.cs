using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Widgets.Utils;

internal enum WindowStyles
{
    Acrylic,
    Solid
}

internal enum WindowSizes
{
    Default,
    Widget
}

internal class WindowProperties
{
    public WindowStyles WindowStyles { get; set; } = WindowStyles.Solid;
    public WindowSizes WindowSizes { get; set; } = WindowSizes.Default;
    public SystemDecorations SystemDecorations { get; set; } = SystemDecorations.Full;
    public bool ExtendClientArea { get; set; } = false;
}

internal static class WindowExtensions
{
    public static void ApplyWindowProperties(this Window @this, WindowProperties properties)
    {
        switch (properties.WindowStyles)
        {
            case WindowStyles.Acrylic:
                @this.Background = Brushes.Transparent;
                @this.TransparencyLevelHint = [WindowTransparencyLevel.AcrylicBlur, WindowTransparencyLevel.Mica];

                var acrylic = new ExperimentalAcrylicBorder
                {
                    Material = new ExperimentalAcrylicMaterial
                    {
                        BackgroundSource = AcrylicBackgroundSource.Digger,
                        MaterialOpacity = 0.4,
                        TintColor = Colors.Black,
                        TintOpacity = 0.4,
                        FallbackColor = Color.FromArgb(100, 0, 0, 0)
                    }
                };

                switch (@this.Content)
                {
                    case Panel panel:
                        panel.Children.Insert(0, acrylic);
                        break;

                    case Control existing:
                        @this.Content = null;
                        var grid = new Grid();
                        grid.Children.Add(acrylic);
                        grid.Children.Add(existing);
                        @this.Content = grid;
                        break;

                    default:
                        throw new InvalidOperationException("Window must have a Panel or Control as Content.");
                }

                break;
            case WindowStyles.Solid:
                @this.Background = Brushes.DimGray;
                break;
        }
        ;

        switch (properties.WindowSizes)
        {
            case WindowSizes.Default:
                @this.Width = 520;
                @this.Height = 412;
                break;
            case WindowSizes.Widget:
                @this.Width = 250;
                @this.Height = 250;
                break;
        }
        ;

        @this.ExtendClientAreaToDecorationsHint = properties.ExtendClientArea;
        @this.SystemDecorations = properties.SystemDecorations;
        @this.FontFamily = "Agave";
        @this.CanResize = false;
    }

    public static void PositionWindow(this Window @this)
    {
        if (@this.Screens.Primary is not { } primaryScreen)
            return;

        // Get the primary screen's working area
        var screen = primaryScreen.WorkingArea;
        var windowSize = Avalonia.PixelSize.FromSize(@this.ClientSize, primaryScreen.Scaling);

        @this.Position = new Avalonia.PixelPoint(
            screen.Width / 2 - windowSize.Width / 2,
            screen.Height / 2 - windowSize.Height / 2
        );
    }
}
