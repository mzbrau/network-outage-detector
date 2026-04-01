namespace NetworkOutageDetector.Services;

public sealed class HourlyReporter
{
    private readonly OutputService _output;
    private readonly OutageTracker _tracker;
    private readonly DateTime _sessionStartUtc;
    private DateTime _nextReportUtc;

    public HourlyReporter(OutputService output, OutageTracker tracker, DateTime sessionStartUtc)
    {
        _output = output;
        _tracker = tracker;
        _sessionStartUtc = sessionStartUtc;
        _nextReportUtc = GetNextClockHour(sessionStartUtc);
    }

    /// <summary>
    /// Call this after each ping. If a clock-hour boundary has passed, prints the report.
    /// </summary>
    public void Check(DateTime nowUtc)
    {
        while (nowUtc >= _nextReportUtc)
        {
            var hourEnd = _nextReportUtc;
            var hourStart = hourEnd.AddHours(-1);

            // For the first hour, the range starts at session start
            var rangeStart = hourStart < _sessionStartUtc ? _sessionStartUtc : hourStart;
            var rangeSeconds = (hourEnd - rangeStart).TotalSeconds;

            if (rangeSeconds > 0)
            {
                var downtime = _tracker.GetDowntime(rangeStart, hourEnd);
                var downtimeSeconds = downtime.TotalSeconds;
                var uptimePercent = ((rangeSeconds - downtimeSeconds) / rangeSeconds) * 100;

                var localStart = rangeStart.ToLocalTime().ToString("HH:mm");
                var localEnd = hourEnd.ToLocalTime().ToString("HH:mm");

                _output.LogBlank();
                _output.Log($"── Hourly Report ({localStart}–{localEnd}) ──────────────────");
                _output.Log($"  Uptime: {uptimePercent:F1}% | Downtime: {(int)downtimeSeconds}s");
                _output.Log("─────────────────────────────────────────────────");
            }

            _nextReportUtc = _nextReportUtc.AddHours(1);
        }
    }

    private static DateTime GetNextClockHour(DateTime utcNow)
    {
        return new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, utcNow.Hour, 0, 0, DateTimeKind.Utc)
            .AddHours(1);
    }
}
