using FileTransformer.Console.Interfaces;

namespace FileTransformer.Console.Services;

public class TransformService(ILogger<TransformService> logger) : ITransformService
{
    public async Task<bool> MakeTransformation(TransformOptions options)
    {
        try
        {
            logger.Log("Starting transformation...");

            var filesToTransform = GetFilesToTransform(options);

            foreach (var filePath in filesToTransform)
            {
                await TransformFile(filePath, options);
            }

            logger.Log($"Transformed files count is {filesToTransform.Length}");
        }
        catch (Exception ex)
        {
            logger.Log($"Error occured: {ex.Message}");
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
        logger.Log($"--- Processing file '{filePath}' ---");

        var newFilePath = filePath.Replace(options.ExtensionFrom, options.ExtensionTo);

        using var reader = new StreamReader(filePath);
        await using var writer = new StreamWriter(newFilePath) ;
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
        writer.Close();
        reader.Close();
        
        logger.Log($"--- Completed processing file ---");
    }
}