using ClientApplication.Graphics;
using ClientApplication.Properties;
using ClientApplication.Scenes;
using GuiPrimitives;
using SFML.Graphics;

public static class Program
{
    public static void Main(string[] args)
    {
        using (var window = new ClientWindow())
        {
            window.Drawable = new Menu(window);
            window.Run();
        }
    }
}