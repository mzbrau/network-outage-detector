namespace NetworkOutageDetector.Services;

public sealed class OutputService : IDisposable
{
    private readonly object _lock = new();
    private StreamWriter? _fileWriter;
    private string _currentLogDate = string.Empty;
    private bool _fileLoggingFailed;

    public void Log(string message)
    {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        var line = $"[{timestamp}] {message}";

        lock (_lock)
        {
            Console.WriteLine(line);
            WriteToFile(line);
        }
    }

    public void LogBlank()
    {
        lock (_lock)
        {
            Console.WriteLine();
            WriteToFile(string.Empty);
        }
    }

    private void WriteToFile(string line)
    {
        if (_fileLoggingFailed)
            return;

        try
        {
            EnsureFileWriter();
            _fileWriter?.WriteLine(line);
            _fileWriter?.Flush();
        }
        catch (IOException)
        {
            HandleFileFailure();
        }
        catch (UnauthorizedAccessException)
        {
            HandleFileFailure();
        }
    }

    private void EnsureFileWriter()
    {
        var today = DateTime.Now.ToString("yyyy-MM-dd");
        if (today == _currentLogDate && _fileWriter is not null)
            return;

        _fileWriter?.Dispose();
        var filename = $"outage-log-{today}.txt";
        _fileWriter = new StreamWriter(filename, append: true) { AutoFlush = false };
        _currentLogDate = today;
    }

    private void HandleFileFailure()
    {
        _fileLoggingFailed = true;
        _fileWriter?.Dispose();
        _fileWriter = null;
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        Console.WriteLine($"[{timestamp}] [WARNING] File logging unavailable — continuing with console only.");
    }

    public void Dispose()
    {
        lock (_lock)
        {
            _fileWriter?.Flush();
            _fileWriter?.Dispose();
            _fileWriter = null;
        }
    }
}
