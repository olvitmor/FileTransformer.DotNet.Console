using FileTransformer.DotNet.Console.Interfaces;

namespace FileTransformer.DotNet.Console.Services;

public class LoggerService<T> : ILogger<T>
{
    public void Log(string message) =>  System.Console.WriteLine($"[{DateTime.Now}] [{typeof(T)}] {message}");
}