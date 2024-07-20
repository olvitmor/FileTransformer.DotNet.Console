namespace FileTransformer;

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
    
    public bool SaveLogs { get;  set; }

    public void Apply(string basePath, bool includeSubDirs,
        string[] patternFrom, string[] patternTo, string extensionFrom, string extensionTo)
    {
        BasePath = basePath;
        IncludeSubDirs = includeSubDirs;
        PatternsFrom = patternFrom;
        PatternsTo = patternTo;
        ExtensionFrom = extensionFrom;
        ExtensionTo = extensionTo;
    }
}