using FileTransformer.Console.Interfaces;

namespace FileTransformer.Console.Validators;

public class PatternsValidator : IParamValidator
{
    private readonly string[] _paramsToHandle;

    public PatternsValidator()
    {
        _paramsToHandle = [nameof(TransformOptions.PatternsFrom), nameof(TransformOptions.PatternsTo)];
    }

    public bool CanHandle(string paramName) => _paramsToHandle.Contains(paramName);

    public (bool, string?) Validate(string? paramValue)
    {
        if (string.IsNullOrEmpty(paramValue))
        {
            return (false, "Pattern should not be empty");
        }

        return (true, null);
    }
}