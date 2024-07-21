using FileTransformer.DotNet.Console.Interfaces;

namespace FileTransformer.DotNet.Console.Services;

public class OptionsHandlerService(IValidator validator) : IOptionsHandlerService
{
    public (bool, string?, string?) TrySetWithValidation(TransformOptions options, string? basePathText,
        string? includeSubDirsText, string? patternFromText, string? patternToText, string? extensionFromText,
        string? extensionToText)
    {
        var (isValid, errorMessage) = validator.Validate(nameof(TransformOptions.BasePath), basePathText);
        if (!isValid) return (false, nameof(TransformOptions.BasePath), errorMessage);

        options.BasePath = basePathText!;
        options.IncludeSubDirs = (includeSubDirsText?.ToLower() != "false");

        (isValid, errorMessage) = validator.Validate(nameof(TransformOptions.PatternsFrom), patternFromText);
        if (!isValid) return (false, nameof(TransformOptions.PatternsFrom), errorMessage);
        
        (isValid, errorMessage) = validator.Validate(nameof(TransformOptions.PatternsTo), patternToText);
        if (!isValid) return (false, nameof(TransformOptions.PatternsTo), errorMessage);

        var patternsFromArray = GetArrayBySeparator(patternFromText!);
        var patternsToArray = GetArrayBySeparator(patternToText!);

        if (patternsFromArray.Length != patternsToArray.Length)
            return (false, "Patterns", $"{nameof(TransformOptions.PatternsFrom)} count should be equal to {nameof(TransformOptions.PatternsTo)} count");

        options.PatternsFrom = patternsFromArray;
        options.PatternsTo = patternsToArray;

        (isValid, errorMessage) = validator.Validate(nameof(TransformOptions.ExtensionFrom), extensionFromText);
        if (!isValid) return (false, nameof(TransformOptions.ExtensionFrom), errorMessage);

        options.ExtensionFrom = extensionFromText!.StartsWith($".") ? extensionFromText : $".{extensionFromText}";
        
        (isValid, errorMessage) = validator.Validate(nameof(TransformOptions.ExtensionTo), extensionToText);
        if (!isValid) return (false, nameof(TransformOptions.ExtensionTo), errorMessage);

        options.ExtensionTo = extensionToText!.StartsWith($".") ? extensionToText : $".{extensionToText}";

        return (true, null, null);
    }

    private string[] GetArrayBySeparator(string from) => from.Split(";");
}