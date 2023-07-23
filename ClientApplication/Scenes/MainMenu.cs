using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientApplication.Core.Scene;
using ClientApplication.Forms;
using ClientApplication.Graphics;
using SFML.Graphics;

namespace ClientApplication.Scenes
{
    internal class MainMenu : Scene
    {
        Menu m_menu;
        public MainMenu(ClientWindow window)
        {
            this.m_menu = new Menu(window);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            //scene

            //ui
            this.m_menu.Draw(target, states);
        }

        public override void Dispose()
        {

        }
    }
}
