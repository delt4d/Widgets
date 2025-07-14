using System;

namespace Widgets.Features.Widget;

[AttributeUsage(AttributeTargets.Class)]
public class WidgetLauncherAttribute(string name) : Attribute
{
    public string Type { get; } = name;
}
