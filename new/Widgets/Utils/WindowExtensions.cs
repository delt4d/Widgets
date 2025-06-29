using Avalonia.Controls;
using Avalonia.Media;
using System;

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
    public static WindowProperties Default() => new();
}

internal static class WindowExtensions
{
    public static void ApplyWindowProperties(this Window @this, WindowProperties properties)
    {
        switch(properties.WindowStyles)
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
        };

        switch(properties.WindowSizes)
        {
            case WindowSizes.Default:
                @this.Width = 250;
                @this.SizeToContent = SizeToContent.Height;
                break;
            case WindowSizes.Widget:
                @this.Width = 250;
                @this.Height = 250;
                break;
        };

        @this.ExtendClientAreaToDecorationsHint = properties.ExtendClientArea;
        @this.SystemDecorations = properties.SystemDecorations;
        @this.FontFamily = "Agave";
        @this.CanResize = false;
    }
}
