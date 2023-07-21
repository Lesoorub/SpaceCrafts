using System.Diagnostics;
using ClientApplication.Graphics;
using SFML.Graphics;
using SFML.System;

namespace GraphicsFundation.Graphics.Forms
{
    public class Form : Control
    {
        Stopwatch clock = new Stopwatch();
        ClientWindow window;

        public Form(ClientWindow window)
        {
            this.window = window;
            this.Size = (Vector2f)window.Size;
            this.window.OnResized += this.Window_OnResized;
        }
        private void Window_OnResized(object? sender, SFML.Window.SizeEventArgs e)
        {
            this.Size = (Vector2f)this.window.Size;
        }

        public override void Dispose()
        {
            this.window.OnResized -= this.Window_OnResized;
            base.Dispose();
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            float deltaTime = (float)this.clock.ElapsedTicks / TimeSpan.TicksPerMillisecond;
            this.clock.Restart();
            this.Update(deltaTime);
            base.Update(deltaTime);
            base.Draw(target, states);
        }
    }
}
