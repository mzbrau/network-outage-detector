using System.Net.NetworkInformation;

namespace NetworkOutageDetector.Services;

public sealed class PingService
{
    private readonly string _target;
    private readonly int _timeoutMs;

    public PingService(string target, int timeoutMs = 500)
    {
        _target = target;
        _timeoutMs = timeoutMs;
    }

    public string Target => _target;

    public async Task<bool> PingAsync()
    {
        using var pinger = new Ping();
        try
        {
            var reply = await pinger.SendPingAsync(_target, _timeoutMs);
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
