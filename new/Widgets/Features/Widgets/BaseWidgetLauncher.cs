using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace Widgets.Features;

public abstract class BaseWidgetLauncher
{
    public required string Title { get; set; }
    public abstract string Type { get; }
    public abstract Task ExecuteAsync(CancellationToken? cancellationToken = null);
}
