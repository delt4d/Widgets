using System;
using System.Diagnostics;

namespace Widgets.Utils;

public static class ProcessCommandUtils
{
    public static void RunCommand(string command, string workingDir, bool background = false)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = $"/c {command}",
                WorkingDirectory = workingDir,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.Start();

        if (!background)
            process.WaitForExit();

        if (!background && process.ExitCode != 0)
            throw new Exception($"Command failed: {command}");
    }
}
