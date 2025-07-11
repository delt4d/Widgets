using System.Collections.Generic;
using Newtonsoft.Json;

namespace Widgets.Features;

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
