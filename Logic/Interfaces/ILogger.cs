using Data.Interfaces;
using Logic.Services;

namespace Logic.Interfaces;

public interface ILogger
{
    void Log(string message, LogLevel level = LogLevel.Info);
    void Log(IBall b1);
    void Log(IBall b1, IBall b2);
    void Log(IBall b1, int total);
    void Info(String message);
    void Warn(String message);
    void Error(String message);
    void Debug(String message);
    void Stop();
}