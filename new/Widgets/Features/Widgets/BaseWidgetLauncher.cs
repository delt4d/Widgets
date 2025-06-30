using System.Threading;
using System.Threading.Tasks;

namespace Widgets.Features;

public abstract class BaseWidgetLauncher
{
    public required string Name { get; set; }
    public abstract string Type { get; }
    public abstract Task ExecuteAsync(CancellationToken? cancellationToken = null);
}
