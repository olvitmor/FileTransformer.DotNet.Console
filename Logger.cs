using FileTransformer.Interfaces;

namespace FileTransformer;

public class Logger<T> : ILogger<T>
{
    public void Log(string message) =>  Console.WriteLine($"[{DateTime.Now}] [{typeof(T)}] {message}");
}