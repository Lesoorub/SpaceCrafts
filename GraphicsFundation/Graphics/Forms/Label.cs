using GraphicsFundation.Properties;
using SFML.Graphics;
using SFML.System;

namespace GraphicsFundation.Graphics.Forms
{
    public class Label : Control
    {
        static Font s_defaultFont = new Font(Resources.consolas);
        private Text text = new Text(string.Empty, Label.s_defaultFont);

        public override FloatRect Bounds => this.text.GetGlobalBounds();

        public HorizontalAligment Horizontal;
        public VerticalAligment Vertical;

        private Vector2f text_offset
        {
            get
            {
                Vector2f p;
                this.text.Position = p = this.GlobalPosition;
                var b = this.Bounds;
                return new Vector2f(b.Left - p.X, b.Top - p.Y);
            }
        }

        public string Text
        {
            get => this.text.DisplayedString;
            set => this.text.DisplayedString = value;
        }
        public Color ForeColor
        {
            get => this.text.FillColor;
            set => this.text.FillColor = value;
        }
        public Color OutlineColor
        {
            get => this.text.OutlineColor;
            set => this.text.OutlineColor = value;
        }
        public float OutlineThickness
        {
            get => this.text.OutlineThickness;
            set => this.text.OutlineThickness = value;
        }
        public uint FontSize
        {
            get => this.text.CharacterSize;
            set => this.text.CharacterSize = value;
        }

        public override void Dispose()
        {
            this.text.Dispose();
            base.Dispose();
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            var pos = this.GlobalPosition - this.text_offset;
            if (this.Horizontal != HorizontalAligment.None ||
                this.Vertical != VerticalAligment.None)
            {
                var b = this.Bounds;
                switch (this.Horizontal)
                {
                    case HorizontalAligment.Middle:
                        pos.X += (this.Size.X - b.Width) / 2f;
                        break;
                    case HorizontalAligment.Right:
                        pos.X += this.Size.X - b.Width;
                        break;
                }
                switch (this.Vertical)
                {
                    case VerticalAligment.Middle:
                        pos.Y += (this.Size.Y - b.Height) / 2f;
                        break;
                    case VerticalAligment.Bottom:
                        pos.Y += this.Size.Y - b.Height;
                        break;
                }
            }
            this.text.Position = pos;
            target.Draw(this.text);
            base.Draw(target, states);
        }
        public enum HorizontalAligment
        {
            None = 0,
            Left = 1,
            Middle = 2,
            Right = 3,
        }
        public enum VerticalAligment
        {
            None = 0,
            Top = 1,
            Middle = 2,
            Bottom = 3,
        }
    }
}
