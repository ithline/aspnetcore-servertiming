namespace Ithline.AspNetCore.ServerTiming;

/// <summary>
/// Represents a server timing record.
/// </summary>
public sealed class ServerTimingMetric
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ServerTimingMetric"/> class.
    /// </summary>
    /// <param name="name">Metric name.</param>
    /// <param name="duration">Metric duration in milliseconds.</param>
    /// <param name="description">Metric description.</param>
    /// <exception cref="ArgumentException"><paramref name="name"/> is <see langword="null"/>, empty or consists only from white-space characters.</exception>
    public ServerTimingMetric(string name, double? duration = null, string? description = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name;
        Duration = duration;
        Description = description;
    }

    /// <summary>
    /// Gets the metric name.
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// Gets the metric duration in milliseconds.
    /// </summary>
    public double? Duration { get; }
    /// <summary>
    /// Gets the metric description.
    /// </summary>
    public string? Description { get; }

    /// <inheritdoc/>
    public override string ToString()
    {
        var result = Name;

        if (!string.IsNullOrWhiteSpace(Description))
        {
            result += FormattableString.Invariant($";desc=\"{Description}\"");
        }

        if (Duration is double duration)
        {
            result += FormattableString.Invariant($";dur={duration}");
        }

        return result;
    }
}
