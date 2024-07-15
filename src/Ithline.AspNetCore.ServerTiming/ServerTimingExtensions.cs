using Microsoft.Extensions.Primitives;

namespace Ithline.AspNetCore.ServerTiming;

/// <summary>
/// Provides a set of extension methods to ease use of <see cref="IServerTiming" />.
/// </summary>
public static class ServerTimingExtensions
{
    /// <summary>
    /// Records a new metric whose value is already obtained.
    /// </summary>
    /// <param name="st">The <see cref="IServerTiming" /> to add metric to.</param>
    /// <param name="name">Metric name.</param>
    /// <param name="duration">Metric duration in milliseconds.</param>
    /// <param name="description">Metric description.</param>
    public static void Add(this IServerTiming st, string name, double? duration = null, string? description = null)
    {
        st.Add(new ServerTimingMetric(name, duration, description));
    }

    /// <summary>
    /// Starts a new server-timing timer.
    /// </summary>
    /// <param name="st">The <see cref="IServerTiming" /> to add metric to.</param>
    /// <param name="name">Metric name.</param>
    /// <param name="description">Metric description.</param>
    /// <returns>A <see cref="IDisposable" /> that completes the recording.</returns>
    public static IDisposable Start(this IServerTiming st, string name, string? description = null)
    {
        return new ServerTimingScope(st, name, description);
    }

    internal static StringValues ToHeaderValue(this IEnumerable<ServerTimingMetric> metrics)
    {
        return metrics.Select(t => t.ToString()).ToArray();
    }

    private sealed class ServerTimingScope : IDisposable
    {
        private readonly ValueStopwatch _sw;
        private readonly string _name;
        private readonly string? _description;
        private IServerTiming? _timing;

        public ServerTimingScope(IServerTiming timing, string name, string? description)
        {
            _timing = timing;
            _name = name;
            _description = description;
            _sw = ValueStopwatch.StartNew();
        }

        public void Dispose()
        {
            var timing = Interlocked.Exchange(ref _timing, null);
            if (timing is not null)
            {
                var elapsed = _sw.ElapsedMilliseconds;
                timing.Add(new ServerTimingMetric(_name, duration: elapsed, description: _description));
            }
        }
    }
}
