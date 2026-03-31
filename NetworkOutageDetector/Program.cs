using NetworkOutageDetector.Services;

namespace NetworkOutageDetector;

class Program
{
    static async Task Main(string[] args)
    {
        var target = ParseTarget(args);

        using var output = new OutputService();
        var pingService = new PingService(target);
        var tracker = new OutageTracker(output);
        var sessionStartUtc = DateTime.UtcNow;
        var reporter = new HourlyReporter(output, tracker, sessionStartUtc);

        using var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            cts.Cancel();
        };

        output.Log($"Network Outage Detector started. Target: {target}");
        output.Log($"Pinging {target} every 1s (timeout: 500ms, threshold: 3 failures)");
        output.Log("Press Ctrl+C to stop.");
        output.LogBlank();

        try
        {
            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
            while (await timer.WaitForNextTickAsync(cts.Token))
            {
                var success = await pingService.PingAsync();
                var now = DateTime.UtcNow;

                tracker.RecordResult(success, now);
                reporter.Check(now);
            }
        }
        catch (OperationCanceledException)
        {
            // Graceful shutdown
        }

        tracker.PrintSummary();
        output.Log("Shutting down.");
    }

    private static string ParseTarget(string[] args)
    {
        const string defaultTarget = "8.8.8.8";

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] is "--target" or "-t" && i + 1 < args.Length)
                return args[i + 1];
        }

        // First positional argument
        if (args.Length > 0 && !args[0].StartsWith('-'))
            return args[0];

        return defaultTarget;
    }
}
