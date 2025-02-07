using FileTransformer.DotNet.Console.Interfaces;
using Terminal.Gui;

namespace FileTransformer.DotNet.Console.Windows;

public sealed class MenuWindow : Window
{
    private readonly TransformOptions _options;
    private readonly ITransformService _transformService;
    private readonly IOptionsHandlerService _optionsHandlerService;

    public MenuWindow(TransformOptions options, ITransformService transformService,
        IOptionsHandlerService optionsHandlerService)
    {
        _options = options;
        _transformService = transformService;
        _optionsHandlerService = optionsHandlerService;
        
        ColorScheme = new Terminal.Gui.ColorScheme
        {
            Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Green, Terminal.Gui.Color.Black),
            HotNormal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightGreen, Terminal.Gui.Color.Black),
            Focus = new Terminal.Gui.Attribute(Terminal.Gui.Color.White, Terminal.Gui.Color.Black),
            HotFocus = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightGreen, Terminal.Gui.Color.Magenta),
            Disabled = new Terminal.Gui.Attribute(Terminal.Gui.Color.Gray, Terminal.Gui.Color.Black)
        };

        Title = "File transformer App (Ctrl+Q to quit)";

        var (basePathLabel, basePathText) = CreateLabelAndTextField("Base folder path:", options.BasePath);

        var (includeSubDirsLabel, includeSubDirsText) = CreateLabelAndTextField("Include sub directories (true/false):",
            options.IncludeSubDirs.ToString(), basePathLabel, basePathText);

        var (patternFromLabel, patternFromText) =
            CreateLabelAndTextField("Patterns FROM:", options.PatternsFromString, includeSubDirsLabel, includeSubDirsText);

        var (patternToLabel, patternToText) =
            CreateLabelAndTextField("Patterns TO:", options.PatternsToString, patternFromLabel, patternFromText);

        var (extensionFromLabel, extensionFromText) =
            CreateLabelAndTextField("Extension FROM:", options.ExtensionFrom, patternToLabel, patternToText);

        var (extensionToLabel, extensionToText) =
            CreateLabelAndTextField("Extension TO:", options.ExtensionTo, extensionFromLabel, extensionFromText);

        var btnExecute = new Button()
        {
            Text = "Execute",
            Y = Pos.Bottom(extensionToLabel) + 1,
            X = Pos.Center(),
            IsDefault = true
        };

        var btnHelp = new Button()
        {
            Text = "Help",
            Y = btnExecute.Y,
            X = Pos.Right(btnExecute) + 1
        };

        btnExecute.Clicked += async () =>
            await OnBtnExecuteClicked(basePathText, includeSubDirsText, patternFromText, patternToText,
                extensionFromText, extensionToText);

        btnHelp.Clicked += OnBtnHelpClicked;

        Add(
            basePathLabel, basePathText,
            includeSubDirsLabel, includeSubDirsText,
            patternFromLabel, patternFromText,
            patternToLabel, patternToText,
            extensionFromLabel, extensionFromText,
            extensionToLabel, extensionToText,
            btnExecute, btnHelp);
    }

    private void OnBtnHelpClicked()
    {
        MessageBox.Query("Help",
            @"
Base folder path -  use path to existing folder like 'D:\Files',

Include sub directories -  should the program check subfolders (enabled by default),

Patterns FROM - original template (use ';' as separator),

Patterns TO - replacement template (use ';' as separator),

Extension FROM - original file extension,

Extension TO - result file extension,

You can see logs after using the keyboard shortcut CTRL + Q

", "Ok");
    }

    private async Task OnBtnExecuteClicked(TextField basePathText, TextField includeSubDirsText,
        TextField patternFromText, TextField patternToText,
        TextField extensionFromText, TextField extensionToText)
    {
        var (isValid, errorTitle, errorMessage) =
            _optionsHandlerService.TrySetWithValidation(
                _options,
                basePathText.Text.ToString(),
                includeSubDirsText.Text.ToString(),
                patternFromText.Text.ToString(),
                patternToText.Text.ToString(),
                extensionFromText.Text.ToString(),
                extensionToText.Text.ToString());

        if (!isValid)
        {
            MessageBox.ErrorQuery($"Validation error: '{errorTitle}'", errorMessage, "Ok");
            return;
        }

        var filesToUpdate = _transformService.CountFilesToTransform(_options);
        
        var response = MessageBox.Query($"Prompt execution",
            $"About {filesToUpdate} files will be transformed, are you confirm?", ["yes", "no"]);

        if (response == 0)
        {
            var isSuccess = await _transformService.MakeTransformation(_options);

            if (!isSuccess)
            {
                MessageBox.ErrorQuery("An error occurred during execution, use the logs to track", errorMessage, "Ok");
            }
            else
            {
                MessageBox.Query("Success", $"{filesToUpdate} files was updated", "Ok");
            }
        }
    }

    private (Label, TextField) CreateLabelAndTextField(string labelText, string textFieldText, Label? prevLabel = null,
        TextField? prevTextField = null)
    {
        var label = new Label()
        {
            Text = labelText
        };

        if (prevLabel != null)
        {
            label.X = Pos.Left(prevLabel);
            label.Y = Pos.Bottom(prevLabel);
        }

        var textField = new TextField(textFieldText)
        {
            X = Pos.Right(label) + 1,
            Width = Dim.Fill()
        };

        if (prevTextField != null)
        {
            textField.Y = Pos.Bottom(prevTextField);
        }

        return (label, textField);
    }

    // private int GetFilesToTransformCount(string? dir = null)
    // {
    //     var totalCount = 0;
    //
    //     if (string.IsNullOrEmpty(dir))
    //     {
    //         dir = TransformOptions.BasePath;
    //     }
    //
    //     totalCount += Directory.GetFiles(dir, $"*{TransformOptions.ExtensionFrom}").Length;
    //
    //     if (TransformOptions.IncludeSubDirs)
    //     {
    //         var subDirs = Directory.GetDirectories(dir);
    //
    //         foreach (var subDir in subDirs)
    //         {
    //             totalCount += GetFilesToTransformCount(subDir);
    //         }
    //     }
    //
    //     return totalCount;
    // }
}