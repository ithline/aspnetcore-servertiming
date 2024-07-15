using Ithline.AspNetCore.ServerTiming;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// The <see cref="IApplicationBuilder"/> extensions for adding Server Timing functionality.
/// </summary>
public static class ServerTimingApplicationBuilderExtensions
{
    /// <summary>
    /// Adds a server timing middleware to application pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <returns><paramref name="app"/></returns>
    public static IApplicationBuilder UseServerTiming(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        return app.UseMiddleware<ServerTimingMiddleware>();
    }
}
