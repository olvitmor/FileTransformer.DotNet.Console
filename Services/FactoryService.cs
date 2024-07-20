using FileTransformer.Interfaces;
using FileTransformer.Validators;
using FileTransformer.Windows;
using Terminal.Gui;

namespace FileTransformer.Services;

public class FactoryService
{
    #region Fields

    private TransformOptions? _options = null;

    private ITransformService? _transformService = null;

    private IValidator? _validator = null;

    private Window? _menuWindow = null;

    #endregion

    #region Constructor

    public FactoryService()
    {
    }

    #endregion

    #region Methods

    public Window GetMenuWindow()
    {
        if (_menuWindow == null)
        {
            _menuWindow = new MenuWindow(GetOptions(), GetTransformService());
        }
        
        return _menuWindow;
    }
    
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