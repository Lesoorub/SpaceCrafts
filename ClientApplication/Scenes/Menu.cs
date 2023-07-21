using System.Text;
using ClientApplication.Graphics;
using ClientApplication.Graphics.UI;
using ClientApplication.Properties;
using GraphicsFundation.Graphics.Forms;
using SFML.Graphics;
using SFML.System;

namespace ClientApplication.Scenes
{
    internal partial class Menu : Form
    {
        public Menu(ClientWindow window) : base(window)
        {
            this.InitComponents();
        }


        public override void Update(float deltaTime)
        {
            if (this.label1 == null) return;
            var b = this.label1.Bounds;
            this.rect2.GlobalPosition = new Vector2f(b.Left, b.Top);
            this.rect2.Size = new Vector2f(b.Width, b.Height);
        }
    }
}