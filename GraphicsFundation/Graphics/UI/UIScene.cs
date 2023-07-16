using SFML.Graphics;
using SFML.System;
using System.Linq;

namespace ClientApplication.Graphics.UI
{
    public class UIScene : Drawable
    {
        public readonly List<UIElement> childrens = new List<UIElement>();

        public bool TryRaycast(Vector2f position, out UIElement hit)
        {
            hit = this.childrens
                .Where(x => x.Visible && x.IsHit(position))
                .OrderBy(x => x.Z)
                .FirstOrDefault()!;
            return hit != null;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (var drawable in this.childrens
                .Where(x => x.Visible)
                .OrderBy(x => x.Z))
                drawable.Draw(target, states);
        }
    }
}
