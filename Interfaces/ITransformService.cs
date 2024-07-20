namespace FileTransformer.Interfaces;

public interface ITransformService
{
    public Task MakeTransformation(TransformOptions options);

    public int CountFilesToTransform(TransformOptions options);
}