using System.Text;
using ClientApplication.Graphics;
using ClientApplication.Properties;
using GraphicsFundation.Graphics.Forms;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ClientApplication.Forms
{
    internal partial class Menu : Form
    {
        public Menu(ClientWindow window) : base(window)
        {
            this.InitComponents();
        }


        public override void Update(float deltaTime)
        {
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
        }
        private void button1_Click(object sender, Mouse.Button e)
        {

        }
    }
}