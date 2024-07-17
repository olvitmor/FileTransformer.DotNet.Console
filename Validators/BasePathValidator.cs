using FileTransformer.Interfaces;

namespace FileTransformer.Validators;

public class BasePathValidator : IParamValidator
{
    private readonly string _paramNameToHandle;

    public BasePathValidator()
    {
        _paramNameToHandle = nameof(TransformOptions.BasePath);
    }

    public bool CanHandle(string paramName) => paramName == _paramNameToHandle;
    
    public (bool, string?) Validate(string? paramValue)
    {
        if (string.IsNullOrEmpty(paramValue))
        {
            return (false, "Directory should not be empty");
        }
        
        if (!Directory.Exists(paramValue))
        {
            return (false, $"Directory does not exists: {paramValue}");
        }

        return (true, null);
    }
}