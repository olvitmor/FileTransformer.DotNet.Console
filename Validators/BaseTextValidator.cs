using FileTransformer.Console.Interfaces;

namespace FileTransformer.Console.Validators;

public class BaseTextValidator : IParamValidator
{
    public bool CanHandle(string paramName) => true;

    public (bool, string?) Validate(string? paramValue)
    {
        if (string.IsNullOrEmpty(paramValue))
        {
            return (false, "Parameter can not be empty");
        }

        return (true, null);
    }
}