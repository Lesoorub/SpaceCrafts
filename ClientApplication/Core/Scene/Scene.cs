using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace ClientApplication.Core.Scene
{
    public abstract class Scene : Drawable, IDisposable
    {
        public abstract void Draw(RenderTarget target, RenderStates states);
        public abstract void Dispose();
    }
}
