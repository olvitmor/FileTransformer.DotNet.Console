using Terminal.Gui;

namespace FileTransformer;

static class Program
{
    static async Task Main(string[] args)
    {
        Application.Run<MenuWindow>();
        
        Application.Shutdown();

        Console.Clear();

        if (TransformOptions.OptionsApplied)
        {
            var transformManager = new TransformManager();

            await transformManager.Transform();

            Console.ReadLine();   
        }
    }
}