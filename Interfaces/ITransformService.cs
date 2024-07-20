namespace FileTransformer.Interfaces;

public interface ITransformService
{
    public Task<bool> MakeTransformation(TransformOptions options);

    public int CountFilesToTransform(TransformOptions options);
}