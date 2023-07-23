using SFML.System;

namespace GraphicsFundation.Graphics.Forms
{
    public struct Padding
    {
        public float Left;
        public float Top;
        public float Right;
        public float Bottom;

        public float Horizontal => this.Left + this.Right;
        public float Vertical => this.Top + this.Bottom;

        public Padding(float left, float top, float right, float bottom)
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }

        public Vector2f ApplyTo(Vector2f size)
        {
            return new Vector2f(size.X - this.Horizontal, size.Y - this.Vertical);
        }
    }
}
