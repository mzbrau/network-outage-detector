using System.Net.NetworkInformation;

namespace NetworkOutageDetector.Services;

public sealed class PingService
{
    private readonly int _timeoutMs;

    public PingService(int timeoutMs = 500)
    {
        _timeoutMs = timeoutMs;
    }

    /// <summary>
    /// Pings all targets in parallel. Returns true if ANY target responds (network is up).
    /// </summary>
    public async Task<bool> PingAllAsync(string[] targets)
    {
        var tasks = targets.Select(t => PingOneAsync(t));
        var results = await Task.WhenAll(tasks);
        return results.Any(r => r);
    }

    private async Task<bool> PingOneAsync(string target)
    {
        using var pinger = new Ping();
        try
        {
            var reply = await pinger.SendPingAsync(target, _timeoutMs);
            return reply.Status == IPStatus.Success;
        }
        catch (PingException)
        {
            return false;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }
}
