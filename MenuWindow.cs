using FileTransformer.Interfaces;
using FileTransformer.Validators;
using Terminal.Gui;

namespace FileTransformer;

public class MenuWindow : Window
{
    private readonly IValidator _validator = new Validator();

    public MenuWindow()
    {
        Title = "File transformer App (Ctrl+Q to quit)";

        var basePathLabel = new Label()
        {
            Text = "Base folder path:"
        };

        var basePathText = new TextField("")
        {
            X = Pos.Right(basePathLabel) + 1,
            Width = Dim.Sized(100)
        };

        var includeSubDirsLabel = new Label()
        {
            Text = "Include sub directories (true/false):",
            X = Pos.Left(basePathLabel),
            Y = Pos.Bottom(basePathLabel)
        };

        var includeSubDirsText = new TextField()
        {
            X = Pos.Right(includeSubDirsLabel) + 1,
            Y = Pos.Bottom(basePathText),
            Width = Dim.Sized(100)
        };

        var patternFromLabel = new Label()
        {
            Text = "Pattern FROM:",
            X = Pos.Left(includeSubDirsLabel),
            Y = Pos.Bottom(includeSubDirsLabel)
        };

        var patternFromText = new TextField("")
        {
            X = Pos.Right(patternFromLabel) + 1,
            Y = Pos.Bottom(includeSubDirsText),
            Width = Dim.Sized(100)
        };

        var patternToLabel = new Label()
        {
            Text = "Pattern TO:",
            X = Pos.Left(patternFromLabel),
            Y = Pos.Bottom(patternFromLabel)
        };

        var patternToText = new TextField("")
        {
            X = Pos.Right(patternToLabel) + 1,
            Y = Pos.Bottom(patternFromText),
            Width = Dim.Sized(100)
        };

        var extensionFromLabel = new Label()
        {
            Text = "Extension FROM:",
            X = Pos.Left(patternToLabel),
            Y = Pos.Bottom(patternToLabel)
        };

        var extensionFromText = new TextField("")
        {
            X = Pos.Right(extensionFromLabel) + 1,
            Y = Pos.Bottom(patternToText),
            Width = Dim.Sized(100)
        };

        var extensionToLabel = new Label()
        {
            Text = "Extension TO:",
            X = Pos.Left(extensionFromLabel),
            Y = Pos.Bottom(extensionFromLabel)
        };

        var extensionToText = new TextField("")
        {
            X = Pos.Right(extensionFromLabel) + 1,
            Y = Pos.Bottom(extensionFromText),
            Width = Dim.Sized(100)
        };

        var btnExecute = new Button()
        {
            Text = "Execute",
            Y = Pos.Bottom(extensionToText) + 1,
            X = Pos.Center(),
            IsDefault = true
        };

        btnExecute.Clicked += () =>
        {
            var basePath = basePathText.Text.ToString();

            var (isValid, errorMessage) =
                _validator.Validate(nameof(TransformOptions.BasePath), basePath);

            if (!isValid)
            {
                MessageBox.ErrorQuery($"Validation error [{nameof(TransformOptions.BasePath)}]", errorMessage,
                    "Ok");
                return;
            }

            var includeSubDirs = includeSubDirsText.Text.ToLower() == "true";

            var patternsFrom = (patternFromText.Text.ToString() ?? "").Split(";");

            (isValid, errorMessage) =git 
                _validator.Validate(nameof(TransformOptions.PatternsFrom), patternsFrom);

            if (!isValid)
            {
                MessageBox.ErrorQuery($"Validation error [{nameof(TransformOptions.PatternsFrom)}]", errorMessage,
                    "Ok");
                return;
            }

            var patternTo = patternToText.Text.ToString();

            (isValid, errorMessage) =
                _validator.Validate(nameof(TransformOptions.PatternsTo), patternTo);

            if (!isValid)
            {
                MessageBox.ErrorQuery($"Validation error [{nameof(TransformOptions.PatternsTo)}]", errorMessage,
                    "Ok");
                return;
            }

            var patternsTo = patternTo!.Split(";");

            var extensionFrom = extensionFromText.Text.ToString();

            (isValid, errorMessage) =
                _validator.Validate(nameof(TransformOptions.ExtensionFrom), extensionFrom);

            if (!isValid)
            {
                MessageBox.ErrorQuery($"Validation error [{nameof(TransformOptions.ExtensionFrom)}]", errorMessage,
                    "Ok");
                return;
            }

            var extensionTo = extensionToText.Text.ToString();

            (isValid, errorMessage) =
                _validator.Validate(nameof(TransformOptions.ExtensionTo), extensionTo);

            if (!isValid)
            {
                MessageBox.ErrorQuery($"Validation error [{nameof(TransformOptions.ExtensionTo)}]", errorMessage,
                    "Ok");
                return;
            }

            TransformOptions.Apply(basePath!,
                includeSubDirs,
                patternsFrom!,
                patternsTo!,
                extensionFrom!,
                extensionTo!);

            var filesToTransform = GetFilesToTransformCount();

            var response = MessageBox.Query($"Prompt execution",
                $"About {filesToTransform} files will be transformed, are you confirm?", ["yes", "no"]);

            if (response == 0)
            {
                Application.RequestStop();
                return;
            }
        };

        Add(
            basePathLabel, basePathText,
            includeSubDirsLabel, includeSubDirsText,
            patternFromLabel, patternFromText,
            patternToLabel, patternToText,
            extensionFromLabel, extensionFromText,
            extensionToLabel, extensionToText,
            btnExecute);
    }
    
    private int GetFilesToTransformCount(string? dir = null)
    {
        var totalCount = 0;

        if (string.IsNullOrEmpty(dir))
        {
            dir = TransformOptions.BasePath;
        }

        totalCount += Directory.GetFiles(dir, $"*{TransformOptions.ExtensionFrom}").Length;

        if (TransformOptions.IncludeSubDirs)
        {
            var subDirs = Directory.GetDirectories(dir);

            foreach (var subDir in subDirs)
            {
                totalCount += GetFilesToTransformCount(subDir);
            }
        }

        return totalCount;
    }
}