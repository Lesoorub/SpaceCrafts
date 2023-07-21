using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuilderGUI.Controls;
using Color = SFML.Graphics.Color;

namespace BuilderGUI.Scenes
{
    internal partial class TestScene : Scene
    {
        public TestScene()
        {
            this.InitializeComponent();
        }
    }
    internal partial class TestScene
    {
        public Label? label1;
        public void InitializeComponent()
        {
            //
            // label1
            //
            this.label1 = new Label();
            this.label1.Text = "Hello world";
            this.label1.ForeColor = Color.Magenta;
            this.label1.LocalPostion = new SFML.System.Vector2f(100, 100);
            this.label1.FontSize = 36;

            this.AddChild(this.label1);
        }
    }
}
