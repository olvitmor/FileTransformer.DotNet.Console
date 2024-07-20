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

    private IOptionsHandlerService? _optionsHandlerService = null;

    #endregion

    #region Constructor

    public FactoryService()
    {
    }

    #endregion

    #region Methods

    public IOptionsHandlerService GetOptionsHandlerService()
    {
        if (_optionsHandlerService == null)
        {
            _optionsHandlerService = new OptionsHandlerService(GetValidator());
        }

        return _optionsHandlerService;
    }

    public Window GetMenuWindow()
    {
        if (_menuWindow == null)
        {
            _menuWindow = new MenuWindow(GetOptions(), GetTransformService(), GetOptionsHandlerService());
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
                    new PatternsValidator()
                });
        }

        return _validator;
    }

    private TransformOptions GetOptions()
    {
        if (_options == null)
        {
#if DEBUG
            _options = new TransformOptions()
            {
                BasePath = @"D:\Projects\ConsoleApps\FileTransformer\Data",
                IncludeSubDirs = true,
                PatternsFrom = ["SN1", "SN2", "SN3"],
                PatternsTo = ["change_1", "change_2", "change_3"],
                ExtensionFrom = "WFD",
                ExtensionTo = "testFileExt"
            };
#else
            _options = new TransformOptions();
#endif
        }

        return _options;
    }

    private ITransformService GetTransformService()
    {
        if (_transformService == null)
        {
            _transformService = new TransformService(new LoggerService<TransformService>());
        }

        return _transformService;
    }

    #endregion
}