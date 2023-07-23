using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace GraphicsFundation.Graphics.Forms.Layouts
{
    public class HorizontalSplit : Control
    {
        public float Spacing;
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            var c = this.Controls;
            if (c.Length == 0) return;
            var childsRegion = this.Padding.ApplyTo(this.Size);

            var targetChildSize = childsRegion;
            targetChildSize.X = (childsRegion.X - this.Spacing * (c.Length - 1)) / c.Length;
            for (int k = 0; k < c.Length; k++)
            {
                ref var child = ref c[k];
                child.Dock = Dock.None;
                child.Size = child.Margin.ApplyTo(targetChildSize);
                child.LocalPosition = new Vector2f(
                    x: k * (targetChildSize.X + this.Spacing) + this.Padding.Left + child.Margin.Left, 
                    y: this.Padding.Top + child.Margin.Top);
            }
        }
    }
}
