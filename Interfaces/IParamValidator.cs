namespace FileTransformer.Interfaces;

public interface IParamValidator
{
    public bool CanHandle(string paramName);

    public (bool, string?) Validate(string? paramValue);
}