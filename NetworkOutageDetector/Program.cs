using NetworkOutageDetector.Services;

namespace NetworkOutageDetector;

class Program
{
    static readonly string[] DefaultTargets = ["8.8.8.8", "1.1.1.1"];

    static async Task Main(string[] args)
    {
        var targets = ParseTargets(args);
        if (targets.Length == 0)
        {
            Console.Error.WriteLine("Error: No valid targets specified.");
            Environment.Exit(1);
            return;
        }
        var targetsDisplay = string.Join(", ", targets);

        using var output = new OutputService();
        var pingService = new PingService();
        var tracker = new OutageTracker(output);
        var sessionStartUtc = DateTime.UtcNow;
        var reporter = new HourlyReporter(output, tracker, sessionStartUtc);

        using var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            cts.Cancel();
        };

        output.Log($"Network Outage Detector started. Targets: {targetsDisplay}");
        output.Log($"Pinging every 1s (timeout: 500ms, threshold: 3 failures)");
        output.Log("Press Ctrl+C to stop.");
        output.LogBlank();

        try
        {
            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
            while (await timer.WaitForNextTickAsync(cts.Token))
            {
                var success = await pingService.PingAllAsync(targets);
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

    private static string[] ParseTargets(string[] args)
    {
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] is "--targets" && i + 1 < args.Length)
                return args[i + 1].Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }

        // Positional arguments
        var positional = args.Where(a => !a.StartsWith('-')).ToArray();
        if (positional.Length > 0)
            return positional;

        return DefaultTargets;
    }
}
