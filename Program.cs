using FileTransformer.Services;
using FileTransformer.Windows;
using Terminal.Gui;

namespace FileTransformer;

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

        Console.ReadKey();
    }
}