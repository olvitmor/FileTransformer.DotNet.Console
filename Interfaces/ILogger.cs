namespace FileTransformer.Interfaces;

public interface ILogger : IDisposable
{
    public void Log(string message);

    public Task SaveLogs();
}