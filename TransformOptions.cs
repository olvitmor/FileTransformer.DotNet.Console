namespace FileTransformer;

public static class TransformOptions
{
    public static string BasePath { get; private set; }

    public static bool IncludeSubDirs { get; private set; } = true;

    public static string[] PatternsFrom { get; private set; }

    public static string[] PatternsTo { get; private set; }

    public static string ExtensionFrom { get; private set; }

    public static string ExtensionTo { get; private set; }

    public static bool OptionsApplied { get; private set; }

    public static void Apply(string basePath, bool includeSubDirs,
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