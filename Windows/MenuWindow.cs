using FileTransformer.Interfaces;
using FileTransformer.Validators;
using Terminal.Gui;

namespace FileTransformer.Windows;

public class MenuWindow : Window
{
    private readonly TransformOptions _options;
    private readonly ITransformService _transformService;

    public MenuWindow(TransformOptions options, ITransformService transformService)
    {
        _options = options;
        _transformService = transformService;

        Title = "File transformer App (Ctrl+Q to quit)";

        var (basePathLabel, basePathText) = CreateLabelAndTextField("Base folder path:", "");

        var (includeSubDirsLabel, includeSubDirsText) = CreateLabelAndTextField("Include sub directories (y):",
            "", basePathLabel, basePathText);

        var (patternFromLabel, patternFromText) =
            CreateLabelAndTextField("Pattern FROM:", "", includeSubDirsLabel, includeSubDirsText);

        var (patternToLabel, patternToText) =
            CreateLabelAndTextField("Pattern TO:", "", patternFromLabel, patternFromText);

        var (extensionFromLabel, extensionFromText) =
            CreateLabelAndTextField("Extension FROM:", "", patternToLabel, patternToText);

        var (extensionToLabel, extensionToText) =
            CreateLabelAndTextField("Extension TO:", "", extensionFromLabel, extensionFromText);

        var (saveLogsLabel, saveLogsText) =
            CreateLabelAndTextField("Save logs (y):", "", extensionToLabel, extensionToText);

        var btnExecute = new Button()
        {
            Text = "Execute",
            Y = Pos.Bottom(saveLogsLabel) + 1,
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
            await OnBtnExecuteClicked(basePathText, patternFromText, patternToText, extensionFromText, extensionToText);

        btnHelp.Clicked += OnBtnHelpClicked;

        Add(
            basePathLabel, basePathText,
            includeSubDirsLabel, includeSubDirsText,
            patternFromLabel, patternFromText,
            patternToLabel, patternToText,
            extensionFromLabel, extensionFromText,
            extensionToLabel, extensionToText,
            saveLogsLabel, saveLogsText,
            btnExecute, btnHelp);
    }

    private void OnBtnHelpClicked()
    {
        MessageBox.Query("Help", 
@"
Base folder path -  use path to existing folder like 'D:\Files',

Include sub directories -  should the program check subfolders (enabled by default),

Pattern FROM - template for searching for the original substring,

Pattern TO - replacement template,

Extension FROM - original file extension,

Extension TO - result file extension,

Save logs - save execution logs to file near program

",
            "Ok");
    }

    private async Task OnBtnExecuteClicked(TextField basePathText, TextField patternFromText, TextField patternToText,
        TextField extensionFromText, TextField extensionToText)
    {
        // var basePath = basePathText.Text.ToString();
        //
        //     var (isValid, errorMessage) =
        //         _validator.Validate(nameof(TransformOptions.BasePath), basePath);
        //
        //     if (!isValid)
        //     {
        //         MessageBox.ErrorQuery($"Validation error [{nameof(TransformOptions.BasePath)}]", errorMessage,
        //             "Ok");
        //         return;
        //     }
        //
        //     var includeSubDirs = includeSubDirsText.Text.ToLower() == "true";
        //
        //     var patternsFrom = (patternFromText.Text.ToString() ?? "").Split(";");
        //
        //     // (isValid, errorMessage) =
        //     //     _validator.Validate(nameof(TransformOptions.PatternsFrom), patternsFrom);
        //
        //     if (!isValid)
        //     {
        //         MessageBox.ErrorQuery($"Validation error [{nameof(TransformOptions.PatternsFrom)}]", errorMessage,
        //             "Ok");
        //         return;
        //     }
        //
        //     var patternTo = patternToText.Text.ToString();
        //
        //     (isValid, errorMessage) =
        //         _validator.Validate(nameof(TransformOptions.PatternsTo), patternTo);
        //
        //     if (!isValid)
        //     {
        //         MessageBox.ErrorQuery($"Validation error [{nameof(TransformOptions.PatternsTo)}]", errorMessage,
        //             "Ok");
        //         return;
        //     }
        //
        //     var patternsTo = patternTo!.Split(";");
        //
        //     var extensionFrom = extensionFromText.Text.ToString();
        //
        //     (isValid, errorMessage) =
        //         _validator.Validate(nameof(TransformOptions.ExtensionFrom), extensionFrom);
        //
        //     if (!isValid)
        //     {
        //         MessageBox.ErrorQuery($"Validation error [{nameof(TransformOptions.ExtensionFrom)}]", errorMessage,
        //             "Ok");
        //         return;
        //     }
        //
        //     var extensionTo = extensionToText.Text.ToString();
        //
        //     (isValid, errorMessage) =
        //         _validator.Validate(nameof(TransformOptions.ExtensionTo), extensionTo);
        //
        //     if (!isValid)
        //     {
        //         MessageBox.ErrorQuery($"Validation error [{nameof(TransformOptions.ExtensionTo)}]", errorMessage,
        //             "Ok");
        //         return;
        //     }
        //
        //     TransformOptions.Apply(basePath!,
        //         includeSubDirs,
        //         patternsFrom!,
        //         patternsTo!,
        //         extensionFrom!,
        //         extensionTo!);
        //
        //     var filesToTransform = GetFilesToTransformCount();
        //
        //     var response = MessageBox.Query($"Prompt execution",
        //         $"About {filesToTransform} files will be transformed, are you confirm?", ["yes", "no"]);
        //
        //     if (response == 0)
        //     {
        //         Application.RequestStop();
        //         return;
        //     }
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