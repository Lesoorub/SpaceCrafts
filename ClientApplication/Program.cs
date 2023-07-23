using System.Runtime.CompilerServices;
using ClientApplication.Core.Scene;
using ClientApplication.Forms;
using ClientApplication.Graphics;
using ClientApplication.Properties;
using ClientApplication.Scenes;
using GuiPrimitives;
using SFML.Graphics;
using SFML.Window;

public static class Program
{
    public static void Main(string[] args)
    {
        using (var window = new ClientWindow())
        {
            window.Displayed += Window_Displayed;

            SceneManager.SetScene(new MainMenu(window));
            window.Drawable = SceneManager.Scene;
            window.Run();

            window.Displayed -= Window_Displayed;
        }
    }

    private static void Window_Displayed(object? sender, float e)
    {
        if (sender is ClientWindow window)
        {
            SceneManager.TrySwitchScene();
            window.Drawable = SceneManager.Scene;
        }
    }
}