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
        Label label1;
        Button button1;
        Picture image1;
        Picture image2;
        Picture image3;
        public void InitComponents()
        {
            //this.rect1 = new Rectangle();
            //this.rect1.Size = new Vector2f(100, 100);
            //this.rect1.FillColor = Color.Green;
            //this.rect1.Dock = Dock.VerticalMiddle;
            //this.AddChild(this.rect1);

            //this.rect3 = new Rectangle();
            //this.rect3.Size = new Vector2f(100, 100);
            //this.rect3.FillColor = Color.Red;
            //this.rect3.Dock = Dock.HorizontalMiddle;
            //this.rect1.AddChild(this.rect3);

            this.button1 = new Button();
            this.button1.GlobalPosition = new Vector2f(100, 100);
            this.button1.Size = new Vector2f(200, 50);
            this.button1.Label.Text = "Next size mode";
            this.button1.Label.FontSize = 24;
            this.button1.Click += (s, e) =>
            {
                this.image1.Size = new Vector2f(100, 200);
                this.image2.Size = new Vector2f(200, 200);
                this.image3.Size = new Vector2f(200, 100);
                var vals = (PictureStretch[])Enum.GetValues(typeof(PictureStretch));
                this.image1.SizeMode = 
                this.image2.SizeMode = 
                this.image3.SizeMode = vals[((int)this.image1.SizeMode + 1) % vals.Length];
                this.label1.Text = this.image3.SizeMode.ToString();
            };
            this.AddChild(this.button1);

            this.label1 = new Label();
            this.label1.Text = "Text";
            this.label1.Horizontal = Label.HorizontalAligment.Middle;
            this.label1.Vertical = Label.VerticalAligment.None;
            this.label1.ForeColor = Color.White;
            this.label1.Dock = Dock.Fill;
            this.AddChild(this.label1);

            //this.rect2 = new Rectangle();
            //this.rect2.FillColor = Color.Transparent;
            //this.rect2.OutlineColor = Color.Magenta;
            //this.rect2.OutlineThickness = 1;
            //this.label1.AddChild(this.rect2);

            this.image1 = new Picture();
            this.image1.LocalPosition = new(100, 250);
            this.image1.Size = new Vector2f(100, 200);
            this.image1.TexturePath = "Assets/engines.png";
            this.image1.SpriteRect = new IntRect(0, 0, 32, height: 32);
            this.AddChild(this.image1);

            this.image2 = new Picture();
            this.image2.LocalPosition = new(250, 250);
            this.image2.Size = new Vector2f(200, 200);
            this.image2.TexturePath = "Assets/engines.png";
            this.image2.SpriteRect = new IntRect(0, 0, 32, height: 32);
            this.AddChild(this.image2);

            this.image3 = new Picture();
            this.image3.LocalPosition = new(500, 250);
            this.image3.Size = new Vector2f(200, 100);
            this.image3.TexturePath = "Assets/engines.png";
            this.image3.SpriteRect = new IntRect(0, 0, 32, height: 32);
            this.AddChild(this.image3);
        }
    }
}
