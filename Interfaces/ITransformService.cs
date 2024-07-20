namespace FileTransformer.Interfaces;

public interface ITransformService
{
    public Task MakeTransformation(TransformOptions options);

    public Task CountFilesToTransform(TransformOptions options);
}