using System.Text;
using ClientApplication.Graphics;
using ClientApplication.Properties;
using GraphicsFundation.Graphics.Forms;
using SFML.Graphics;
using SFML.System;

namespace ClientApplication.Scenes
{
    internal partial class Menu : Form
    {
        RectangleShape rect;
        public Menu(ClientWindow window) : base(window)
        {
            this.rect = new RectangleShape(new Vector2f(100, 100));
            this.rect.FillColor = Color.Blue;
            this.rect.Position = new Vector2f(0, 0);
            this.InitComponents();
        }


        public override void Update(float deltaTime)
        {
            this.window.CameraPosition += new Vector2f(-1f, 0);
            if (this.label1 == null) return;
            var b = this.label1.Bounds;
            this.rect2.GlobalPosition = new Vector2f(b.Left, b.Top);
            this.rect2.Size = new Vector2f(b.Width, b.Height);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(this.rect, states);
            base.Draw(target, states);
        }
    }
}