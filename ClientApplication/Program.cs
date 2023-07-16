using ClientApplication.Graphics;
using ClientApplication.Scenes;

public static class Program
{
    public static void Main(string[] args)
    {
        using (var window = new ClientWindow())
        {
            window.Drawable = new Menu();
            window.Run();
        }
    }
}