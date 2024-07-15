namespace Ithline.AspNetCore.ServerTiming;

/// <summary>
/// Provides means to record server timing metrics.
/// </summary>
public interface IServerTiming : IReadOnlyCollection<ServerTimingMetric>
{
    /// <summary>
    /// Records a new metric whose value is already obtained.
    /// </summary>
    /// <param name="metric">Metric to record.</param>
    /// <exception cref="ArgumentException"><paramref name="metric"/> is <see langword="null"/>, empty or consists only from white-space characters.</exception>
    void Add(ServerTimingMetric metric);
}
