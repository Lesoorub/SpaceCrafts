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
            SceneManager.SetScene(new MainMenu(window));
            window.Drawable = SceneManager.Scene;
            window.Run();
        }
    }
}