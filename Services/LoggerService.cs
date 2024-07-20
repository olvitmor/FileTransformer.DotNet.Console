using FileTransformer.Interfaces;

namespace FileTransformer.Services;

public class LoggerService<T> : ILogger<T>
{
    public void Log(string message) =>  Console.WriteLine($"[{DateTime.Now}] [{typeof(T)}] {message}");
}