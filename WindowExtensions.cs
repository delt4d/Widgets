using System;
using Avalonia.Controls;
using Avalonia.Media;

namespace widgets;

public static class WindowExtensions
{
    public static void ApplyWidgetDefaultProperties(this Window @this)
    {
        @this.Width = 250;
        @this.Height = 250;
    }

    public static void ApplyDefaultWindowProperties(this Window @this)
    {
        @this.Background = Brushes.Transparent;
        @this.CanResize = false;
        @this.ExtendClientAreaToDecorationsHint = true;
        @this.TransparencyLevelHint = [WindowTransparencyLevel.AcrylicBlur];

        var acrylic = new ExperimentalAcrylicBorder
        {
            IsHitTestVisible = false,
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

        if (@this.Content is Panel panel)
        {
            panel.Children.Insert(0, acrylic);
            return;
        }

        if (@this.Content is Control existing)
        {
            @this.Content = null;
            panel = new StackPanel
            {
                Margin = new Avalonia.Thickness(10)
            };
            panel.Children.AddRange([acrylic, existing]);
            @this.Content = panel;
            return;
        }

        throw new Exception("Window must have a Panel or Control.");
    }
}
