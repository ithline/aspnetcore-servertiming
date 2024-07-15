using Ithline.AspNetCore.ServerTiming;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extensions methods for adding server timing services to the DI container.
/// </summary>
public static class ServerTimingServiceCollectionExtensions
{
    /// <summary>
    /// Adds services required for server timing.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add the services to.</param>
    /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
    public static IServiceCollection AddServerTiming(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.TryAddScoped<IServerTiming, ServerTiming>();

        return services;
    }
}
