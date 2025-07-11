using Avalonia.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using WebViewControl;
using Widgets.Screens.Views;

namespace Widgets.Features;

[AttributeUsage(AttributeTargets.Class)]
public class WidgetLauncherAttribute(string name) : Attribute
{
    public string Type { get; } = name;
}

[AttributeUsage(AttributeTargets.Property)]
public class WidgetAdditionalAttribute(string key) : Attribute
{
    public string Key { get; } = key;
}

[JsonObject(MemberSerialization.OptOut)]
public record WidgetData(string Name, string Title)
{
    public Dictionary<string, object> Additional { get; set; } = [];


    public T GetAdditional<T>(string key)
    {
        if (!Additional.TryGetValue(key, out var value))
            return default!;

        return (T)value;
    }
}

public interface IWidgetLauncher
{
    public string Title { get; set; }
    public WidgetData GetData();
    public IWidgetLauncher SetData(WidgetData data);
    public Task ExecuteAsync(CancellationToken? cancellationToken = null);
}

public abstract class WidgetLauncherBase : IWidgetLauncher
{
    public string Title { get; set; } = string.Empty;

    public WidgetData GetData()
    {
        var type = GetType();
        var launcherAttr = type.GetCustomAttribute<WidgetLauncherAttribute>() ?? 
            throw new InvalidOperationException("Missing WidgetLauncher attribute.");
        
        var additional = type
            .GetProperties()
            .Where(p => p.IsDefined(typeof(WidgetAdditionalAttribute)))
            .ToDictionary(
                p => p.GetCustomAttribute<WidgetAdditionalAttribute>()!.Key,
                p => p.GetValue(this) ?? new object()
            );

        return new WidgetData(launcherAttr.Type, Title)
        {
            Additional = additional
        };
    }

    public IWidgetLauncher SetData(WidgetData data)
    {
        var type = GetType();

        foreach (var prop in type.GetProperties().Where(p => p.IsDefined(typeof(WidgetAdditionalAttribute))))
        {
            var key = prop.GetCustomAttribute<WidgetAdditionalAttribute>()!.Key;

            if (data.Additional != null && data.Additional.TryGetValue(key, out var value))
            {
                var converted = Convert.ChangeType(value, prop.PropertyType);
                prop.SetValue(this, converted);
            }
        }

        Title = data.Title;

        return this;
    }

    public abstract Task ExecuteAsync(CancellationToken? cancellationToken = null);

    #region Static
    static WidgetLauncherBase()
    {
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(IWidgetLauncher).IsAssignableFrom(t) && t.GetCustomAttribute<WidgetLauncherAttribute>() != null);

        foreach (var type in types)
        {
            var attr = type.GetCustomAttribute<WidgetLauncherAttribute>();
            if (attr != null)
            {
                _launchers[attr.Type] = type;
            }
        }
    }
    static readonly Dictionary<string, Type> _launchers = [];
    public static IWidgetLauncher Default => new VoidLauncher();
    public static IWidgetLauncher? CreateLauncher(string name)
    {
        if (_launchers.TryGetValue(name, out var type))
            return (IWidgetLauncher?)Activator.CreateInstance(type);
        return null;
    }
    #endregion
    #region VoidLauncher
    public class VoidLauncher : WidgetLauncherBase
    {
        public override Task ExecuteAsync(CancellationToken? cancellationToken = null)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}


[WidgetLauncher("Webserver")]
public class WebserverWidgetLauncher : WidgetLauncherBase
{
    public WidgetWindow? Window { get; private set; }

    [WidgetAdditional("Url")]
    public string Url { get; set; } = "https://google.com";

    [WidgetAdditional("ReuseWindow")]
    public bool ReuseWindow { get; set; } = false;

    public override Task ExecuteAsync(CancellationToken? cancellationToken = null)
    {
        var WebViewComponent = new WebView
        {
            Address = Url,
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch
        };

        WebViewComponent.SetValue(Grid.RowProperty, 0);
        WebViewComponent.SetValue(Grid.ColumnProperty, 0);

        var widgetWindow = GetWidgetWindow();
        var mainGrid = widgetWindow.FindControl<Grid>("MainGrid");
        mainGrid?.Children.Add(WebViewComponent);

        widgetWindow.Show();

        return Task.CompletedTask;
    }

    private WidgetWindow GetWidgetWindow()
    {
        if (Window is not null && ReuseWindow)
            return Window;

        Window = new WidgetWindow();
        Window.Closed += (_, _) => Window = null;

        return Window;
    }
}

[WidgetLauncher("TimerWidget")]
public class LocalWidgetLauncher : WidgetLauncherBase
{
    public TimerWidgetWindow? Window { get; private set; }

    [WidgetAdditional("ReuseWindow")]
    public bool ReuseWindow { get; set; } = false;

    public override Task ExecuteAsync(CancellationToken? cancellationToken = null)
    {
        var window = GetWidgetWindow();
        window.Show();
        return Task.CompletedTask;
    }

    private TimerWidgetWindow GetWidgetWindow()
    {
        if (Window is not null && ReuseWindow)
            return Window;

        Window = new TimerWidgetWindow();
        Window.Closed += (_, _) => Window = null;

        return Window;
    }
}