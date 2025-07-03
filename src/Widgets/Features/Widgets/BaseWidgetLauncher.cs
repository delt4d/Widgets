using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Widgets.Features;

[JsonObject(MemberSerialization.OptIn)]
public abstract class BaseWidgetLauncher
{
    [JsonProperty]
    public required string Title { get; set; }
    public abstract string Type { get; }
    public abstract Task ExecuteAsync(CancellationToken? cancellationToken = null);
}

public class VoidWidgetLauncher : BaseWidgetLauncher
{
    public override string Type => throw new System.NotImplementedException();
    public override Task ExecuteAsync(CancellationToken? cancellationToken = null) => Task.CompletedTask;
    public static VoidWidgetLauncher Default => new()
    {
        Title = "Void"
    };
}