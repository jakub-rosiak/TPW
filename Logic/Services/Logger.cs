using Logic.Interfaces;

namespace Logic.Services;

public enum LogLevel
{
    Info,
    Warning,
    Error,
    Debug
}

public class Logger : ILogger
{
    private readonly string _logFilePath;
    private static readonly object _lock = new object();

    public Logger()
    {
        string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TPW", "Logs");

        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        _logFilePath = Path.Combine(folder, $"{DateTime.Now:yyyyMMdd}.log");
        Debug($"Logging to {_logFilePath}");
    }

    public void Log(string message, LogLevel level = LogLevel.Info)
    {
        string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [{level}] {message}";
        
        ConsoleColor originalColor = Console.ForegroundColor;
        switch (level)
        {
            case LogLevel.Warning:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case LogLevel.Error:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case LogLevel.Debug:
                Console.ForegroundColor = ConsoleColor.White;
                break;
            default:
                Console.ForegroundColor = originalColor;
                break;
        }
        Console.WriteLine(logEntry);
        Console.ForegroundColor = originalColor;

        lock (_lock)
        {
            File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
        }
    }
    
    public void Info(string message) => Log(message, LogLevel.Info);
    public void Warn(string message) => Log(message, LogLevel.Warning);
    public void Error(String message) => Log(message, LogLevel.Error);
    public void Debug(String message) => Log(message, LogLevel.Debug);
}