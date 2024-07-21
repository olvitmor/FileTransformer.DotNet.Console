namespace FileTransformer.DotNet.Console.Interfaces;

public interface IValidator
{
    public (bool, string?) Validate(string paramName, string? paramValue);
}