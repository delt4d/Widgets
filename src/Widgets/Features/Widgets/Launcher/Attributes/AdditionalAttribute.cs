using System;

namespace Widgets.Features.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class WidgetAdditionalAttribute(string key) : Attribute
{
    public string Key { get; } = key;
}