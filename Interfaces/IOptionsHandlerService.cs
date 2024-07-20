namespace FileTransformer.Interfaces;

public interface IOptionsHandlerService
{
    public (bool, string?, string?) TrySetWithValidation(TransformOptions options, string? basePathText, string? includeSubDirsText, string? patternFromText,
        string? patternToText, string? extensionFromText, string? extensionToText, string? saveLogsText);
}