using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Widgets.Features.Attributes;

namespace Widgets.Features;

public abstract class WidgetLauncherBase : IWidgetLauncher
{
    public string Title { get; set; } = string.Empty;

    public WidgetData GetData()
    {
        var type = GetType();
        var launcherAttr = type.GetCustomAttribute<WidgetLauncherAttribute>() ??
            throw new InvalidOperationException("Missing WidgetLauncher attribute.");

        var args = type
            .GetProperties()
            .Where(p => p.IsDefined(typeof(WidgetArgsAttribute)))
            .ToDictionary(
                p => p.GetCustomAttribute<WidgetArgsAttribute>()!.Key,
                p => p.GetValue(this) ?? new object()
            );

        return new WidgetData(launcherAttr.Type, Title)
        {
            Args = args
        };
    }
    
    public IWidgetLauncher SetData(WidgetData data)
    {
        var type = GetType();

        foreach (var prop in type.GetProperties().Where(p => p.IsDefined(typeof(WidgetArgsAttribute))))
        {
            var key = prop.GetCustomAttribute<WidgetArgsAttribute>()!.Key;

            if (data.Args != null && data.Args.TryGetValue(key, out var value))
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