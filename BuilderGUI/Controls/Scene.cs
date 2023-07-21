using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace BuilderGUI.Controls
{
    internal class Scene : Control
    {
        public override void Draw(RenderTarget window)
        {
            foreach (var child in this.Childrens)
                child.Draw(window);
        }

        public override bool IsHover(float x, float y)
        {
            throw new NotImplementedException();
        }
    }
}
