using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace BuilderGUI.Extentions
{
    public static class FloatRectExtention
    {
        public static FloatRect Expand(this FloatRect rect1, FloatRect rect2)
        {
            float left = Math.Min(rect1.Left, rect2.Left);
            float top = Math.Min(rect1.Top, rect2.Top);

            float right = Math.Max(rect1.Left + rect1.Width, rect2.Left + rect2.Width);
            float bottom = Math.Max(rect1.Top + rect1.Height, rect2.Top + rect2.Height);

            return new FloatRect(left, top, right - left, bottom - top);
        }

    }
}
