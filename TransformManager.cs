namespace FileTransformer;

public class TransformManager
{
    private readonly Logger<TransformManager> _logger = new Logger<TransformManager>();
    
    public async Task Transform()
    {
        try
        {
            _logger.Log("Starting transformation...");
            
            var transformedFilesCount = await TransformFilesInDirectory(TransformOptions.BasePath);
        
            _logger.Log($"Transformed files count is {transformedFilesCount}");
        }
        catch (Exception ex)
        {
            _logger.Log($"Error occured: {ex.Message}");
        }
    }

    private async Task<int> TransformFilesInDirectory(string dirPath)
    {
        _logger.Log($"--- Starting processing directory '{dirPath}' ---");
        
        var transformedFiles = 0;
        
        var filesInCurrentDirectory = Directory.GetFiles(dirPath, $"*{TransformOptions.ExtensionFrom}");

        foreach (var fileName in filesInCurrentDirectory)
        {
            await TransformFile(fileName);
        }

        transformedFiles += filesInCurrentDirectory.Length;

        if (TransformOptions.IncludeSubDirs)
        {
            var subDirs = Directory.GetDirectories(dirPath);

            foreach (var subDir in subDirs)
            {
                transformedFiles += await TransformFilesInDirectory(subDir);
            }
        }
        
        _logger.Log($"--- Directory '{dirPath}' processing ended ---");

        return transformedFiles;
    }

    private async Task TransformFile(string filePath)
    {
        _logger.Log($"  --- Processing file '{filePath}' ---");

        var newFilePath = filePath.Replace(TransformOptions.ExtensionFrom, TransformOptions.ExtensionTo);

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