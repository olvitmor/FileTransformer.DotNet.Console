using FileTransformer.Interfaces;
using FileTransformer.Validators;

namespace FileTransformer.Services;

public class FactoryService
{
    #region Fields

    private TransformOptions? _options = null;

    private ITransformService? _transformService = null;

    private IValidator? _validator = null;

    #endregion

    #region Constructor

    public FactoryService()
    {
    }

    #endregion

    #region Methods

    public IValidator GetValidator()
    {
        if (_validator == null)
        {
            _validator = new Validator(
                new IParamValidator[]
                {
                    new BasePathValidator(),
                    new PatternsValidator(),
                    new ExtensionValidator()
                });
        }

        return _validator;
    }

    public TransformOptions GetOptions()
    {
        if (_options == null)
        {
            _options = new TransformOptions();
        }

        return _options;
    }

    public ITransformService GetTransformService()
    {
        if (_transformService == null)
        {
            _transformService = new TransformService();
        }

        return _transformService;
    }

    #endregion
}