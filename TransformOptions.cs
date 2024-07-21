namespace FileTransformer.Console;

public class TransformOptions
{
    public string BasePath { get; set; } = string.Empty;

    public bool IncludeSubDirs { get;  set; } = true;

    public string PatternsFromString => PatternsFrom is not { Length: > 0 } ? "" : string.Join(";", PatternsFrom);

    public string[] PatternsFrom { get;  set; } = [];
    
    public string PatternsToString => PatternsTo is not { Length: > 0 } ? "" : string.Join(";", PatternsTo);

    public string[] PatternsTo { get;  set; } = [];

    public string ExtensionFrom { get;  set; } = string.Empty;

    public string ExtensionTo { get;  set; } = string.Empty;
}