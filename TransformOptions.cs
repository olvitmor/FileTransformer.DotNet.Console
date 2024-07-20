namespace FileTransformer;

public class TransformOptions
{
    public string BasePath { get; private set; }

    public bool IncludeSubDirs { get; private set; } = true;

    public string[] PatternsFrom { get; private set; }

    public string[] PatternsTo { get; private set; }

    public string ExtensionFrom { get; private set; }

    public string ExtensionTo { get; private set; }

    public bool OptionsApplied { get; private set; }

    public void Apply(string basePath, bool includeSubDirs,
        string[] patternFrom, string[] patternTo, string extensionFrom, string extensionTo)
    {
        BasePath = basePath;
        IncludeSubDirs = includeSubDirs;
        PatternsFrom = patternFrom;
        PatternsTo = patternTo;
        ExtensionFrom = extensionFrom;
        ExtensionTo = extensionTo;
        OptionsApplied = true;
    }
}