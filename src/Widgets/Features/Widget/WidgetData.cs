using System.Collections.Generic;
using Newtonsoft.Json;

namespace Widgets.Features.Widget;

[JsonObject(MemberSerialization.OptOut)]
public record WidgetData(string Name, string Title)
{
    public Dictionary<string, object> Args { get; set; } = [];

    public T GetArgs<T>(string key)
    {
        if (!Args.TryGetValue(key, out var value))
            return default!;

        return (T)value;
    }
}
