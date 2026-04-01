using NetworkOutageDetector.Models;

namespace NetworkOutageDetector.Services;

public sealed class OutageTracker
{
    private readonly OutputService _output;
    private readonly int _failureThreshold;
    private readonly List<Outage> _outages = [];
    private int _consecutiveFailures;
    private DateTime _firstFailureInSequence;
    private Outage? _currentOutage;

    public OutageTracker(OutputService output, int failureThreshold = 3)
    {
        _output = output;
        _failureThreshold = failureThreshold;
    }

    public IReadOnlyList<Outage> Outages => _outages;
    public bool InOutage => _currentOutage is not null;

    public void RecordResult(bool success, DateTime timestamp)
    {
        if (success)
        {
            _consecutiveFailures = 0;
            _firstFailureInSequence = default;

            if (_currentOutage is not null)
            {
                _currentOutage.EndTime = timestamp;
                _output.Log($"✓ OUTAGE ENDED   | {_currentOutage}");
                _currentOutage = null;
            }
        }
        else
        {
            _consecutiveFailures++;
            if (_consecutiveFailures == 1)
                _firstFailureInSequence = timestamp;

            if (_consecutiveFailures == _failureThreshold && _currentOutage is null)
            {
                _currentOutage = new Outage { StartTime = _firstFailureInSequence };
                _outages.Add(_currentOutage);
                _output.Log("⚠ OUTAGE STARTED");
            }
        }
    }

    /// <summary>
    /// Returns total downtime within the given UTC time range.
    /// </summary>
    public TimeSpan GetDowntime(DateTime rangeStart, DateTime rangeEnd)
    {
        var totalDowntime = TimeSpan.Zero;

        foreach (var outage in _outages)
        {
            var outageEnd = outage.EndTime ?? rangeEnd;
            var overlapStart = outage.StartTime > rangeStart ? outage.StartTime : rangeStart;
            var overlapEnd = outageEnd < rangeEnd ? outageEnd : rangeEnd;

            if (overlapStart < overlapEnd)
                totalDowntime += overlapEnd - overlapStart;
        }

        return totalDowntime;
    }

    public void PrintSummary()
    {
        if (_currentOutage is not null)
        {
            _currentOutage.EndTime = DateTime.UtcNow;
            _output.Log($"✓ OUTAGE ENDED   | {_currentOutage} (shutdown)");
        }

        _output.LogBlank();
        _output.Log("── Session Summary ─────────────────────────────");
        _output.Log($"  Total outages: {_outages.Count}");

        var totalDowntime = TimeSpan.Zero;
        foreach (var outage in _outages)
            totalDowntime += outage.Duration;

        _output.Log($"  Total downtime: {(int)totalDowntime.TotalSeconds}s");
        _output.Log("─────────────────────────────────────────────────");
    }
}
