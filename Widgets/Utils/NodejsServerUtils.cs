using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Widgets.Utils;

public static class NodejsServerUtils
{
    public static readonly string ProjectUrl = "http://127.0.0.1:5500/";
    public static readonly string ProjectWorkingDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"../../../../Widgets.SampleWidget"));

    public static async Task<bool> EnsureServerRunningAsync()
    {
        if (await IsServerRunningAsync())
            return true;

        return StartServer();
    }

    public static bool StartServer()
    {
        try
        {
            ProcessCommandUtils.RunCommand("npm install", ProjectWorkingDir);
            ProcessCommandUtils.RunCommand("npm run start", ProjectWorkingDir, background: true);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to start server: {ex.Message}");
            return false;
        }
    }

    private static async Task<bool> IsServerRunningAsync()
    {
        try
        {
            using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(2) };
            var response = await client.GetAsync(ProjectUrl);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
