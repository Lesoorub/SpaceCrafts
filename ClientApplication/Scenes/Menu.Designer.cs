using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicsFundation.Graphics.Forms;
using SFML.Graphics;
using SFML.System;

namespace ClientApplication.Scenes
{
    partial class Menu
    {
        Rectangle rect1;
        Rectangle rect2;
        Rectangle rect3;
        public void InitComponents()
        {
            this.rect1 = new Rectangle();
            this.rect1.Size = new Vector2f(200, 200);
            this.rect1.FillColor = Color.Green;
            this.rect1.Dock = Dock.None;
            this.rect1.Padding = new Padding(5, 5, 5, 5);
            this.AddChild(this.rect1);

            this.rect2 = new Rectangle();
            this.rect2.LocalPosition = new Vector2f(23, 23);
            this.rect2.Size = new Vector2f(50, 50);
            this.rect2.FillColor = Color.Red;
            this.rect2.Margin = new Padding(5, 5, 5, 5);
            this.rect2.Padding = new Padding(5, 5, 5, 5);
            this.rect2.Dock = Dock.Fill;
            this.rect1.AddChild(this.rect2);

            this.rect3 = new Rectangle();
            this.rect3.LocalPosition = new Vector2f(23, 23);
            this.rect3.Size = new Vector2f(10, 10);
            this.rect3.FillColor = Color.Blue;
            this.rect3.Dock = Dock.HorizontalUnclipped;
            this.rect2.AddChild(this.rect3);
        }
    }
}
