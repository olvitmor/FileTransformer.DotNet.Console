using FileTransformer.Interfaces;

namespace FileTransformer.Services;

public class TransformService : ITransformService
{
    private readonly Logger<TransformService> _logger = new Logger<TransformService>();
    
    public async Task MakeTransformation(TransformOptions options)
    {
        try
        {
            _logger.Log("Starting transformation...");
            
            var transformedFilesCount = await TransformFilesInDirectory(options.BasePath, options);
        
            _logger.Log($"Transformed files count is {transformedFilesCount}");
        }
        catch (Exception ex)
        {
            _logger.Log($"Error occured: {ex.Message}");
        }
    }

    public int CountFilesToTransform(TransformOptions options)
    {
        var searchOptions = options.IncludeSubDirs ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

        return Directory.GetFiles(options.BasePath, $"*{options.ExtensionFrom}", searchOptions).Length;
    }

    private async Task<int> TransformFilesInDirectory(string dirPath, TransformOptions options)
    {
        _logger.Log($"--- Starting processing directory '{dirPath}' ---");
        
        var transformedFiles = 0;
        
        var filesInCurrentDirectory = Directory.GetFiles(dirPath, $"*{options.ExtensionFrom}");

        foreach (var fileName in filesInCurrentDirectory)
        {
            await TransformFile(fileName, options);
        }

        transformedFiles += filesInCurrentDirectory.Length;

        if (options.IncludeSubDirs)
        {
            var subDirs = Directory.GetDirectories(dirPath);

            foreach (var subDir in subDirs)
            {
                transformedFiles += await TransformFilesInDirectory(subDir, options);
            }
        }
        
        _logger.Log($"--- Directory '{dirPath}' processing ended ---");

        return transformedFiles;
    }

    private async Task TransformFile(string filePath, TransformOptions options)
    {
        _logger.Log($"  --- Processing file '{filePath}' ---");

        var newFilePath = filePath.Replace(options.ExtensionFrom, options.ExtensionTo);

        using var reader = new StreamReader(filePath);
        await using (var writer = new StreamWriter(newFilePath))
        {
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                // var updatedLine = line?.Replace(TransformOptions.PatternsFrom, TransformOptions.PatternsTo);
                // await writer.WriteLineAsync(updatedLine);
            }

            await writer.FlushAsync();
        }

        reader.Close();
    }
}