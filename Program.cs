using FileTransformer.Windows;
using Terminal.Gui;

namespace FileTransformer;

public static class Program
{
    public static void Main(string[] args)
    {
        Application.Run<MenuWindow>();
        
        Application.Shutdown();

        Console.Clear();
    }
}