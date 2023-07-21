﻿using System;
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
        public void InitComponents()
        {
            this.rect1 = new Rectangle();
            this.rect1.Size = new Vector2f(100, 100);
            this.rect1.FillColor = Color.Green;
            this.AddChild(this.rect1);
            this.rect1.Dock = Dock.VerticalMiddle;

            this.rect3 = new Rectangle();
            this.rect3.Size = new Vector2f(100, 100);
            this.rect3.FillColor = Color.Red;
            this.rect1.AddChild(this.rect3);
            this.rect3.Dock = Dock.HorizontalMiddle;

            //this.label1 = new Label();
            //this.label1.Text = "Hello world";
            //this.label1.Horizontal = Label.HorizontalAligment.Middle;
            //this.label1.Vertical = Label.VerticalAligment.Middle;
            //this.label1.ForeColor = Color.White;
            //this.rect1.AddChild(this.label1);
            //this.label1.Dock = Dock.Fill;

            //this.rect2 = new Rectangle();
            //this.rect2.FillColor = Color.Transparent;
            //this.rect2.OutlineColor = Color.Magenta;
            //this.rect2.OutlineThickness = 1;
            //this.label1.AddChild(this.rect2);
        }
    }
}