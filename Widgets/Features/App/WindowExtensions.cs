using System;
using Avalonia.Controls;
using Avalonia.Media;

namespace widgets.Features.App;

public class DefaultWindowPropertiesConfig
{
    public bool ExtendClientAreaToDecorationsHint { get; set; } = false;
    public SystemDecorations SystemDecorations { get; set; } = SystemDecorations.Full;
    public static DefaultWindowPropertiesConfig Default => new();
}

public static class WindowExtensions
{
    public static void ApplyWidgetDefaultProperties(this Window @this)
    {
        @this.Width = 250;
        @this.Height = 250;
    }

    public static void ApplyMainWidgetProperties(this Window @this)
    {
        @this.Width = 250;
        @this.SizeToContent = SizeToContent.Height;
    }

    public static void ApplyDefaultWindowProperties(this Window @this, DefaultWindowPropertiesConfig? config = null)
    {
        config ??= DefaultWindowPropertiesConfig.Default;
        @this.ExtendClientAreaToDecorationsHint = config.ExtendClientAreaToDecorationsHint;
        @this.SystemDecorations = config.SystemDecorations;
        @this.FontFamily = "agave";
        @this.CanResize = false;
        @this.Background = Brushes.DimGray;
    }

    public static void ApplyDefaultAcrylicProperties(this Window @this)
    {
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
                return;

            case Control existing:
                @this.Content = null;
                var grid = new Grid(); 
                grid.Children.Add(acrylic);
                grid.Children.Add(existing);
                @this.Content = grid;
                return;

            default:
                throw new InvalidOperationException("Window must have a Panel or Control as Content.");
        }
    }

}