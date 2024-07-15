using System.Collections;

namespace Ithline.AspNetCore.ServerTiming;

internal sealed class ServerTiming : IServerTiming
{
    private readonly List<ServerTimingMetric> _metrics = [];

    public ServerTiming()
    {
    }

    public int Count => _metrics.Count;

    public void Add(ServerTimingMetric metric)
    {
        ArgumentNullException.ThrowIfNull(metric);

        _metrics.Add(metric);
    }

    public IEnumerator<ServerTimingMetric> GetEnumerator() => _metrics.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
