using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace ClientApplication.Graphics
{
    public partial class GraphicSettings
    {
        public static GraphicSettings Default { get; set; } = new GraphicSettings();

        public string Title = "SpaceCrafts";
        public WindowSettings CurrentWindowSettings = WindowSettings.Windowed;
        public uint MaxFPS = 60;

        public Resolution CurrentResolution = new Resolution(800, 600);


        public RenderWindow CreateRendererWindow()
        {
            RenderWindow app;
            switch (this.CurrentWindowSettings)
            {
                default:
                    app = new RenderWindow(
                        new VideoMode(
                            this.CurrentResolution.Width,
                            this.CurrentResolution.Height),
                        this.Title);
                    break;
                case WindowSettings.Fullscreen:
                    app = new RenderWindow(
                        VideoMode.DesktopMode, 
                        this.Title, 
                        Styles.Fullscreen);
                    break;
                case WindowSettings.FullscreenInWindow:
                    app = new RenderWindow(
                        VideoMode.DesktopMode, 
                        this.Title, Styles.None);
                    app.Position = new SFML.System.Vector2i(0, 0);
                    break;
            }
            app.SetFramerateLimit(this.MaxFPS);
            return app;
        }
    }
}
