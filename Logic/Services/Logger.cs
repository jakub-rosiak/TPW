﻿using Data.Interfaces;
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
    private List<string> buffer = new List<string>();
    private bool _running = true;

    public Logger()
    {
        string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TPW", "Logs");

        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        _logFilePath = Path.Combine(folder, $"{DateTime.Now:yyyyMMdd}.log");
        
        if (File.Exists(_logFilePath))
        {
            DateTime logFileDate = File.GetLastWriteTime(_logFilePath);
            string timestamp = logFileDate.ToString("yyyyMMdd_HHmmss");
            string backupPath = Path.Combine(folder, $"{timestamp}.log");
            File.Move(_logFilePath, backupPath);
        }
        
        Debug($"Logging to {_logFilePath}");

        Task.Run(() =>
        {
            while (_running)
            {
                WriteToFile();
                Thread.Sleep(1000);
            }

            WriteToFile();
        });
    }

    public void Log(string message, LogLevel level = LogLevel.Info)
    {
        string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [{level}] {message}";
        Console.WriteLine(logEntry);

        lock (_lock)
        {
            buffer.Add(logEntry);
        }
    }
    
    public void Info(string message) => Log(message, LogLevel.Info);
    public void Warn(string message) => Log(message, LogLevel.Warning);
    public void Error(string message) => Log(message, LogLevel.Error);
    public void Debug(string message) => Log(message, LogLevel.Debug);

    public void Log(IBall ball)
    {
        string message = $"Ball {ball.Id} collided with a wall at x: {Math.Round(ball.XPos, 2)}, y: {Math.Round(ball.YPos, 2)}.";
        Debug(message);
    }

    public void Log(IBall b1, IBall b2)
    {
        string message = $"Ball {b1.Id} collided with {b2.Id} at x1: {Math.Round(b1.XPos, 2)}, y1: {Math.Round(b1.YPos, 2)}, x2: {Math.Round(b2.XPos, 2)}, y2: {Math.Round(b2.YPos, 2)}.";
        Debug(message);
    }

    public void Log(IBall b1, int total)
    {
        string message = $"Creating ball at ({b1.Id + 1}/{total}) at x: {Math.Round(b1.XPos, 2)}, y: {Math.Round(b1.YPos, 2)}.";
        Info(message);
    }
    
    private void WriteToFile()
    {
        List<string> bufferCopy = null;
        lock (_lock)
        {
            if (buffer.Count == 0) return;
            
            bufferCopy = new List<string>(buffer);
            buffer.Clear();
        }
        File.AppendAllText(_logFilePath, string.Join(Environment.NewLine, bufferCopy) + Environment.NewLine);
    }

    public void Stop()
    {
        _running = false;
    }
}