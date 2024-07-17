using FileTransformer.Interfaces;

namespace FileTransformer.Validators;

public class ExtensionValidator : IParamValidator
{
    private readonly string[] _paramsToHandle;

    public ExtensionValidator()
    {
        _paramsToHandle = [nameof(TransformOptions.ExtensionFrom), nameof(TransformOptions.ExtensionTo)];
    }

    public bool CanHandle(string paramName) => _paramsToHandle.Contains(paramName);

    public (bool, string?) Validate(string? paramValue)
    {
        if (string.IsNullOrEmpty(paramValue))
        {
            return (false, "Extension should not be empty");
        }

        if (!paramValue.StartsWith('.'))
        {
            return (false, "Extension string should starts with '.'");
        }

        return (true, null);
    }
}