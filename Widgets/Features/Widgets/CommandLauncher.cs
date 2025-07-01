using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Widgets.Features;

public class CommandLauncher : BaseWidgetLauncher
{
    public required string Command { get; set; }
    public override string Type => "command";

    public override async Task ExecuteAsync(CancellationToken? cancellationToken = null)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = $"/C {Command}",
            CreateNoWindow = true,
            UseShellExecute = false
        };

        var ps = Process.Start(psi) ?? throw new Exception("Unable to start process");
        await ps.WaitForExitAsync(cancellationToken ?? CancellationToken.None);
    }
}
