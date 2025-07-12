using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text.Json;
using Widgets.Features;

namespace Widgets.Converters;

//public class LauncherItemConverter : Newtonsoft.Json.JsonConverter
//{
//    public override bool CanConvert(Type objectType)
//    {
//        return objectType == typeof(BaseWidgetLauncher);
//    }

//    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, Newtonsoft.Json.JsonSerializer serializer)
//    {
//        var jo = JObject.Load(reader);
//        var type = jo["Type"]?.ToString();

//        return type switch
//        {
//            "webserver" => jo.ToObject<WebServerLauncher>(serializer),
//            "local" => jo.ToObject<LocalWidgetLauncher>(serializer),
//            "command" => jo.ToObject<CommandLauncher>(serializer),
//            _ => throw new System.Text.Json.JsonException($"Unknown launcher type '{type}'")
//        };
//    }

//    public override void WriteJson(JsonWriter writer, object? value, Newtonsoft.Json.JsonSerializer serializer)
//    {
//        JObject jo = JObject.FromObject(value!, serializer);
//        jo.AddFirst(new JProperty("type", ((BaseWidgetLauncher)value!).Type));
//        jo.WriteTo(writer);
//    }
//}

