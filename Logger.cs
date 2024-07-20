using System.Text;
using FileTransformer.Interfaces;

namespace FileTransformer;

public class Logger<T> : ILogger
{
    private readonly StringBuilder _stringBuilder = new StringBuilder();
    
    public void Log(string message) =>  _stringBuilder.AppendLine($"[{DateTime.Now}] [{typeof(T)}] {message}");
    public async Task SaveLogs()
    {
        var currentFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;
        var currentDate = DateTime.Now.Date;
        var logFileName = $"file-transformer-{currentDate}.log";
        var logFilePath = Path.Combine(currentFolder, logFileName);

        await using var sw = new StreamWriter(logFilePath, true);
        await sw.WriteLineAsync();
        await sw.WriteLineAsync(_stringBuilder.ToString());
        await sw.FlushAsync();
    }

    public void Dispose() => _stringBuilder.Clear();
}