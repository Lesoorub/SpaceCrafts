using SFML.Graphics;
using SFML.System;

namespace GraphicsFundation.Graphics.Forms
{
    public class Rectangle : Control
    {
        RectangleShape rectangle = new RectangleShape();

        public Color FillColor
        {
            get => this.rectangle.FillColor;
            set => this.rectangle.FillColor = value;
        }
        public Color OutlineColor
        {
            get => this.rectangle.OutlineColor;
            set => this.rectangle.OutlineColor = value;
        }
        public float OutlineThickness
        {
            get => this.rectangle.OutlineThickness;
            set => this.rectangle.OutlineThickness = value;
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            this.rectangle.Position = this.GlobalPosition;
            this.rectangle.Size = this.Size;
            target.Draw(this.rectangle);
            base.Draw(target, states);
        }
        public override void Dispose()
        {
            this.rectangle.Dispose();
            base.Dispose();
        }
    }
}
