using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace GraphicsFundation.Graphics.Forms.Layouts
{
    public class VerticalLayout : Control
    {
        public float Spacing;
        public Aligment ChildAligment = Aligment.Middle;
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            float sumHeigth = this.Controls.Sum(x => x.Size.Y + this.Spacing + x.Margin.Top) - this.Spacing;
            var controls = this.Controls;
            foreach (var child in controls)
                child.Dock = Dock.None;
            var zero = new Vector2f(this.Padding.Left, this.Padding.Top);
            float cursor = zero.Y;
            switch (this.ChildAligment)
            {
                case Aligment.Middle:
                    cursor = (this.Size.Y - sumHeigth) / 2f - zero.Y;
                    break;
                case Aligment.Bottom:
                    cursor = this.Size.Y - sumHeigth - zero.Y;
                    break;
            }
            for (int k = 0; k < controls.Length; k++)
            {
                ref var child = ref controls[k];
                child.LocalPosition = new Vector2f(zero.X, cursor);
                child.Size = new Vector2f(this.Size.X - this.Margin.Left - this.Margin.Right, child.Size.Y);
                cursor += child.Size.Y + this.Spacing + child.Margin.Top + child.Margin.Bottom;
            }
        }
        public enum Aligment
        {
            Top,
            Middle,
            Bottom,
        }
    }
}
