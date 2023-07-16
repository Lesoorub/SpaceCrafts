using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientApplication.Graphics.UI;
using ClientApplication.Properties;
using SFML.Graphics;
using SFML.System;

namespace ClientApplication.Scenes
{
    internal class Menu : UIScene
    {
        public Menu()
        {
            base.childrens.Add(UI_Node.from);
        }
    }
}