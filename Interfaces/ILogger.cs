namespace FileTransformer.DotNet.Console.Interfaces;

public interface ILogger<T>
{
    public void Log(string message);
}