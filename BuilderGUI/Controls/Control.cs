using SFML.Graphics;
using SFML.System;

namespace BuilderGUI
{
    public abstract class Control
    {
        public Vector2f LocalPostion;

        private Control? parent;

        List<Control> Controls = new List<Control>();

        public IEnumerable<Control> Childrens => this.Controls;

        public Control? Parent 
        { 
            get => this.parent; 
            set
            {
                if (this.parent == value) return;
                if (this.parent != null) 
                    this.parent.Controls.Remove(this);
                if (value != null)
                    value.AddChild(this);
            }
        }

        public Vector2f GlobalPostion
        {
            get
            {
                if (this.parent != null)
                    return this.parent.GlobalPostion + this.LocalPostion;
                return this.LocalPostion;
            }
            set
            {
                if (this.parent != null)
                    this.LocalPostion = value - this.parent.GlobalPostion;
                this.LocalPostion = value;
            }
        }

        public abstract bool IsHover(float x, float y);
        public abstract void Draw(RenderTarget window);

        public void AddChild(Control control)
        {
            control.parent = this;
            this.Controls.Add(control);
        }
    }
}