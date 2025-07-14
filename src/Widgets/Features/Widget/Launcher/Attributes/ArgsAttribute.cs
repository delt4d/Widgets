using System;

namespace Widgets.Features.Widget;

[AttributeUsage(AttributeTargets.Property)]
public class WidgetArgsAttribute(string key) : Attribute
{
    public string Key { get; } = key;
}