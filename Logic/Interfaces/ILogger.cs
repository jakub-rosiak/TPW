using Logic.Services;

namespace Logic.Interfaces;

public interface ILogger
{
    void Log(string message, LogLevel level = LogLevel.Info);
    void Info(String message);
    void Warn(String message);
    void Error(String message);
    void Debug(String message);
    void Stop();
}