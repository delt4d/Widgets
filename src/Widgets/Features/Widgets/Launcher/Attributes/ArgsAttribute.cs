using System;

namespace Widgets.Features.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class WidgetArgsAttribute(string key) : Attribute
{
    public string Key { get; } = key;
}