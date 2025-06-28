using System;
using Avalonia.Controls;
using Avalonia.Media;

namespace widgets.Features.App;

public static class WindowExtensions
{
    public static void ApplyWidgetDefaultProperties(this Window @this)
    {
        @this.Width = 250;
        @this.Height = 250;
    }

    public static void ApplyDefaultWindowProperties(this Window @this, Action<ApplyDefaultWindowPropertiesConfig>? configure = null)
    {
        var windowProperties = new ApplyDefaultWindowPropertiesConfig{ Transparent = true, ExtendClientAreaToDecorationsHint = false };

        if (configure is not null)
            configure(windowProperties);

        @this.ExtendClientAreaToDecorationsHint = windowProperties.ExtendClientAreaToDecorationsHint;
        @this.FontFamily = "agave";
        @this.CanResize = false;
        @this.Background = Brushes.Transparent;
        @this.TransparencyLevelHint = [WindowTransparencyLevel.AcrylicBlur];

        if (!windowProperties.Transparent)
        {
            @this.Background = Brushes.Gray;
        }

        var acrylic = new ExperimentalAcrylicBorder
        {
            Material = new ExperimentalAcrylicMaterial
            {
                BackgroundSource = AcrylicBackgroundSource.Digger,
                MaterialOpacity = 0.1,
                TintColor = Colors.Black,
                TintOpacity = 1.0,
                FallbackColor = Colors.Black
            }
        };

        // Insert <ExperimentalAcrylicBorder />

        {
            if (@this.Content is Panel panel)
            {
                panel.Children.Insert(0, acrylic);
                return;
            }
        }
        {
            if (@this.Content is Control existing)
            {
                @this.Content = null;
                var panel = new StackPanel { Margin = new Avalonia.Thickness(10) };
                panel.Children.AddRange([acrylic, existing]);
                @this.Content = panel;
                return;
            }
        }

        throw new Exception("Window must have a Panel or Control.");
    }
}

public class ApplyDefaultWindowPropertiesConfig
{
    public required bool Transparent { get; set; }
    public required bool ExtendClientAreaToDecorationsHint { get; set; }
}