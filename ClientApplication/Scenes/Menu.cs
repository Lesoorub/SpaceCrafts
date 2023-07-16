using System.Text;
using ClientApplication.Graphics.UI;
using ClientApplication.Properties;

namespace ClientApplication.Scenes
{
    internal class Menu : UIScene
    {
        public Menu()
        {
            var root = UI_NodeXMLConverter.FromXml(Resources.test);
            base.childrens.Add(root);
        }
    }
}