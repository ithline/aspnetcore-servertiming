using System.Diagnostics;

namespace Ithline.AspNetCore.ServerTiming;

/// <summary>
/// Provides a set of methods and properties that you can use to accurately measure elapsed time.
/// </summary>
internal readonly struct ValueStopwatch
{
    private static readonly double _tickFrequency = TimeSpan.TicksPerSecond / (double)Stopwatch.Frequency;
    private readonly long _startTimestamp;

    private ValueStopwatch(long startTimestamp)
    {
        _startTimestamp = startTimestamp;
    }

    /// <summary>
    /// Gets the total elapsed time measured by the current instance.
    /// </summary>
    public TimeSpan Elapsed => new(this.GetElapsedTicks());

    /// <summary>
    /// Gets the total elapsed time measured by the current instance, in milliseconds.
    /// </summary>
    public long ElapsedMilliseconds => this.GetElapsedTicks() / TimeSpan.TicksPerMillisecond;

    /// <summary>
    /// Gets a value indicating whether the <see cref="ValueStopwatch" /> timer is running.
    /// </summary>
    public bool IsRunning => _startTimestamp != 0;

    /// <summary>
    /// Initializes a new <see cref="ValueStopwatch" /> instance and starts measuring elapsed time.
    /// </summary>
    /// <returns>A <see cref="ValueStopwatch" /> that has just begun measuring elapsed time.</returns>
    public static ValueStopwatch StartNew() => new(Stopwatch.GetTimestamp());

    private long GetElapsedTicks()
    {
        // Start timestamp can't be zero in an initialized ValueStopwatch. It would have to be literally the first thing executed when the machine boots to be 0.
        // So it being 0 is a clear indication of default(ValueStopwatch)
        if (!IsRunning)
        {
            throw new InvalidOperationException("An uninitialized, or 'default', ValueStopwatch cannot be used to get elapsed time.");
        }

        var delta = Stopwatch.GetTimestamp() - _startTimestamp;
        return (long)(delta * _tickFrequency);
    }
}
