namespace NetworkOutageDetector.Models;

public record Outage
{
    public DateTime StartTime { get; init; }
    public DateTime? EndTime { get; set; }

    public TimeSpan Duration => EndTime.HasValue
        ? EndTime.Value - StartTime
        : DateTime.UtcNow - StartTime;

    public bool IsOngoing => !EndTime.HasValue;

    public override string ToString()
    {
        var start = StartTime.ToLocalTime().ToString("HH:mm:ss");
        if (IsOngoing)
            return $"Start: {start} | Ongoing | Duration: {FormatDuration(Duration)}";

        var end = EndTime!.Value.ToLocalTime().ToString("HH:mm:ss");
        return $"Start: {start} | End: {end} | Duration: {FormatDuration(Duration)}";
    }

    private static string FormatDuration(TimeSpan duration)
    {
        if (duration.TotalHours >= 1)
            return $"{(int)duration.TotalHours}h {duration.Minutes}m {duration.Seconds}s";
        if (duration.TotalMinutes >= 1)
            return $"{duration.Minutes}m {duration.Seconds}s";
        return $"{(int)duration.TotalSeconds}s";
    }
}
