using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Widgets.Features.Widget;

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

    public static async IAsyncEnumerable<IWidgetLauncher> LoadAsync()
    {
        var path = GetJsonPath();

        if (!File.Exists(path))
            yield break;

        var json = await File.ReadAllTextAsync(path);
        var items = JsonConvert.DeserializeObject<List<WidgetData>>(json) ?? [];

        foreach (var data in items)
        {
            var launcher = WidgetLauncherBase
                .CreateLauncher(data.Name)?
                .SetData(data) ?? throw new Exception($"No launcher registered for {data.Name}");

            yield return launcher;
        }
    }

    public static async Task SaveAsync(IEnumerable<IWidgetLauncher> items)
    {
        var path = GetJsonPath();
        var json = JsonConvert.SerializeObject(items.Select(x => x.GetData()), Formatting.Indented);
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        await File.WriteAllTextAsync(path, json);
    }
}
