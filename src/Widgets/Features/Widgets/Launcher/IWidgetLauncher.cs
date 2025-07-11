using System.Threading;
using System.Threading.Tasks;

namespace Widgets.Features;

public interface IWidgetLauncher
{
    public string Title { get; set; }
    public WidgetData GetData();
    public IWidgetLauncher SetData(WidgetData data);
    public Task ExecuteAsync(CancellationToken? cancellationToken = null);
}
