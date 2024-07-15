using Microsoft.AspNetCore.Http;

namespace Ithline.AspNetCore.ServerTiming;

internal sealed class ServerTimingMiddleware
{
    private readonly RequestDelegate _next;

    public ServerTimingMiddleware(RequestDelegate next)
    {
        ArgumentNullException.ThrowIfNull(next);

        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IServerTiming serverTiming)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(serverTiming);

        var sw = ValueStopwatch.StartNew();

        if (context.Response.SupportsTrailers())
        {
            context.Response.DeclareTrailer(HeaderNames.ServerTiming);

            await _next(context).ConfigureAwait(false);
            serverTiming.Add("total", sw.ElapsedMilliseconds);

            context.Response.AppendTrailer(HeaderNames.ServerTiming, serverTiming.ToHeaderValue());
        }
        else
        {
            context.Response.OnStarting(() =>
            {
                serverTiming.Add("total", sw.ElapsedMilliseconds);
                context.Response.Headers.Append(HeaderNames.ServerTiming, serverTiming.ToHeaderValue());
                return Task.CompletedTask;
            });

            await _next(context).ConfigureAwait(false);
        }
    }
}
