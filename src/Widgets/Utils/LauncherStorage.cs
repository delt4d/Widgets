using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Widgets.Converters;
using Widgets.Features;

namespace Widgets.Utils;

public static class LauncherStorage
{
    private static readonly string JsonPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "WidgetsApp", "launchers.json");

    private static readonly string DevJsonPath = Path.GetFullPath(Path.Combine(
        AppContext.BaseDirectory, 
        "../../../../launchers.dev.json"));

    private static string GetJsonPath()
    {
        return DevJsonPath;
    }

    public static async Task<List<BaseWidgetLauncher>> LoadAsync()
    {
        var path = GetJsonPath();
        
        if (!File.Exists(path))
            return [];

        var json = await File.ReadAllTextAsync(path);
        return JsonConvert.DeserializeObject<List<BaseWidgetLauncher>>(json, new LauncherItemConverter())
            ?? throw new Exception("Unable to deserialize json");
    }

    public static async Task SaveAsync(IEnumerable<BaseWidgetLauncher> items)
    {
        var path = GetJsonPath();
        var json = JsonConvert.SerializeObject(items, Formatting.Indented);

        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        await File.WriteAllTextAsync(path, json);
    }
}
