using FileTransformer.DotNet.Console.Services;
using Terminal.Gui;

namespace FileTransformer.DotNet.Console;

public static class Program
{
    public static void Main(string[] args)
    {
        var factoryService = new FactoryService();

        Application.Init();
        
        using (var menuWindow = factoryService.GetMenuWindow())
        {
            Application.Run(menuWindow);
        }
        
        Application.Shutdown();

        System.Console.WriteLine("Press ENTER to close this window...");
        System.Console.ReadLine();
    }
}