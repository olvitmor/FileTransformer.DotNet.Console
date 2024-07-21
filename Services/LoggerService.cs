using FileTransformer.Console.Interfaces;

namespace FileTransformer.Console.Services;

public class LoggerService<T> : ILogger<T>
{
    public void Log(string message) =>  System.Console.WriteLine($"[{DateTime.Now}] [{typeof(T)}] {message}");
}