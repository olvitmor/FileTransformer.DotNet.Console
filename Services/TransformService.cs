using FileTransformer.Interfaces;

namespace FileTransformer.Services;

public class TransformService : ITransformService
{
    private Logger<TransformService>? _logger = null;
    
    public async Task<bool> MakeTransformation(TransformOptions options)
    {
        if (options.SaveLogs)
        {
            _logger = new Logger<TransformService>();
        }
        
        try
        {
            _logger?.Log("Starting transformation...");

            var filesToTransform = GetFilesToTransform(options);
            
            foreach (var filePath in filesToTransform)
            {
                await TransformFile(filePath, options);
            }
        
            _logger?.Log($"Transformed files count is {filesToTransform.Length}");
            
            if (options.SaveLogs)
            {
                _logger?.SaveLogs();
                _logger?.Dispose();
            }
        }
        catch (Exception ex)
        {
            _logger?.Log($"Error occured: {ex.Message}");
            return false;
        }

        return true;
    }

    private static string[] GetFilesToTransform(TransformOptions options)
    {
        var searchOptions = options.IncludeSubDirs ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

        return Directory.GetFiles(options.BasePath, $"*{options.ExtensionFrom}", searchOptions);
    }

    public int CountFilesToTransform(TransformOptions options) => GetFilesToTransform(options).Length;

    private async Task TransformFile(string filePath, TransformOptions options)
    {
        _logger?.Log($"  --- Processing file '{filePath}' ---");

        var newFilePath = filePath.Replace(options.ExtensionFrom, options.ExtensionTo);

        using var reader = new StreamReader(filePath);
        await using (var writer = new StreamWriter(newFilePath))
        {
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();

                for (var i = 0; i < options.PatternsFrom.Length; i++)
                {
                    line = line?.Replace(options.PatternsFrom[i], options.PatternsTo[i]);
                }

                await writer.WriteLineAsync(line);
            }

            await writer.FlushAsync();
        }

        reader.Close();
    }
}