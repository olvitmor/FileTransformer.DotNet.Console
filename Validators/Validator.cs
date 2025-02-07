using FileTransformer.DotNet.Console.Interfaces;

namespace FileTransformer.DotNet.Console.Validators;

public class Validator : IValidator
{
    private readonly IParamValidator[] _optionValidators;
    
    public Validator(IParamValidator[] optionValidators)
    {
        _optionValidators = optionValidators;
    }
    
    public (bool, string?) Validate(string paramName, string? paramValue)
    {
        var validator = GetValidatorByParamName(paramName);

        return validator.Validate(paramValue);
    }

    private IParamValidator GetValidatorByParamName(string paramName)
    {
        foreach (var t in _optionValidators)
        {
            if (t.CanHandle(paramName))
                return t;
        }

        return new BaseTextValidator();
    }
}