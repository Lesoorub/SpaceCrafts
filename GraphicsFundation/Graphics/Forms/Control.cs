using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GraphicsFundation.Graphics.Forms
{
    public abstract class Control : Drawable, IDisposable
    {
        private Dock m_dock = Dock.None;
        private List<Control> Childrens = new List<Control>();
        private Control? m_parent;
        private Vector2f m_size;
        private Vector2f m_position;

        event Action? Changed;
        public EventHandler<Mouse.Button>? MousePressed;
        public EventHandler<Mouse.Button>? MouseReleased;
        [Obsolete("Not working")]
        public EventHandler<Mouse.Button>? Click;
        public EventHandler? MouseMoved;
        public EventHandler? MouseEnter;
        public EventHandler? MouseLeave;

        public bool IsMouseHovered { get; private set; }
        public Dock Dock
        {
            get => this.m_dock;
            set
            {
                this.m_dock = value;
                switch (this.m_dock)
                {
                    case Dock.None:
                        break;
                    case Dock.HorizontalUnclipped:
                        if (this.m_parent != null)
                            this.m_size.X = this.m_parent.Size.X;
                        this.m_position.X = 0;
                        break;
                    case Dock.VerticallUnclipped:
                        if (this.m_parent != null)
                            this.m_size.Y = this.m_parent.Size.Y;
                        this.m_position.Y = 0;
                        break;
                    case Dock.Fill:
                        if (this.m_parent != null)
                            this.m_size = this.m_parent.Size;
                        this.m_position = new Vector2f(0, 0);
                        break;
                    case Dock.HorizontalMiddle:
                        this.m_position.X = 0;
                        if (this.m_parent != null)
                        {
                            this.m_size.X = this.m_parent.Size.X;
                            this.m_position.Y = this.m_parent.Size.Y / 2 - this.m_size.Y / 2;
                        }
                        break;
                    case Dock.VerticalMiddle:
                        this.m_position.Y = 0;
                        if (this.m_parent != null)
                        {
                            this.m_size.Y = this.m_parent.Size.Y;
                            this.m_position.X = this.m_parent.Size.X / 2 - this.m_size.X / 2;
                        }
                        break;
                    case Dock.HorizontalTop:
                        this.m_position = new Vector2f(0, 0);
                        if (this.m_parent != null)
                            this.m_size.X = this.m_parent.Size.X;
                        break;
                    case Dock.VerticalLeft:
                        this.m_position = new Vector2f(0, 0);
                        if (this.m_parent != null)
                            this.m_size.Y = this.m_parent.Size.Y;
                        break;
                    case Dock.HorizontalBottom:
                        this.m_position.X = 0;
                        if (this.m_parent != null)
                        {
                            this.m_size.X = this.m_parent.Size.X;
                            this.m_position.Y = this.m_parent.Size.Y - this.m_size.Y;
                        }
                        break;
                    case Dock.VerticalRight:
                        this.m_position.Y = 0;
                        if (this.m_parent != null)
                        {
                            this.m_size.Y = this.m_parent.Size.Y;
                            this.m_position.X = this.m_parent.Size.X - this.m_size.X;
                        }
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
        public virtual Vector2f LocalPosition
        {
            get => this.m_position;
            set
            {
                this.m_position = value;
                this.Changed?.Invoke();
            }
        }
        public virtual Vector2f Size
        {
            get => this.m_size;
            set
            {
                this.m_size = value;
                this.Changed?.Invoke();
            }
        }
        public virtual FloatRect Bounds => new FloatRect(this.GlobalPosition, this.Size);
        public Control? Parent
        {
            get => this.m_parent;
            set
            {
                if (this == value) return;
                if (this.m_parent != null)
                {
                    this.m_parent.Changed -= this.Control_ParentChanged;
                    this.m_parent.Childrens.Remove(this);
                }
                if (value != null)
                {
                    value.AddChild(this);
                }
                this.m_parent = value;
                if (this.m_parent != null)
                    this.m_parent.Changed += this.Control_ParentChanged;
                this.Changed?.Invoke();
            }
        }
        public Control[] Controls => this.Childrens.ToArray();
        public Vector2f GlobalPosition
        {
            get
            {
                if (this.m_parent != null)
                    return this.m_parent.GlobalPosition + this.LocalPosition;
                return this.LocalPosition;
            }
            set
            {
                if (this.m_parent != null)
                {
                    this.LocalPosition = value - this.m_parent.GlobalPosition;
                    return;
                }
                this.LocalPosition = value;
            }
        }
        public Control Root
        {
            get
            {
                Control current = this;
                while (current.m_parent != null)
                    current = current.m_parent;
                return current;
            }
        }
        public virtual bool IsHoverable { get; set; }

        public void AddChild(Control control)
        {
            if (control == this) return;
            control.m_parent = this;
            this.Changed += control.Control_ParentChanged;
            this.Childrens.Add(control);
            this.Changed?.Invoke();
        }
        public virtual bool IsHover(int x, int y)
        {
            var b = this.Bounds;
            return b.Contains(x, y);
        }
        public virtual Control? GetHovered(int x, int y)
        {
            foreach (var child in this.Childrens)
            {
                var c = child.GetHovered(x, y);
                if (c != null)
                    return c;
            }
            if (this.IsHoverable && this.IsHover(x, y))
                return this;
            return null;
        }

        public virtual void Update(float deltaTime)
        {
            foreach (var child in this.Childrens)
                child.Update(deltaTime);
        }
        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            foreach (var child in this.Childrens)
                child.Draw(target, states);
        }
        private void Control_ParentChanged()
        {
            this.Dock = this.m_dock;
            this.Changed?.Invoke();
        }

        public void ProcessMouseMovedEvent(int x, int y)
        {
            if (this.IsHover(x, y))
            {
                this.MouseMoved?.Invoke(this, new EventArgs());
                if (!this.IsMouseHovered)
                {
                    this.IsMouseHovered = true;
                    this.MouseEnter?.Invoke(this, new EventArgs());
                }
            }
            else
            {
                if (this.IsMouseHovered)
                {
                    this.IsMouseHovered = false;
                    this.MouseLeave?.Invoke(this, new EventArgs());
                }
            }

            foreach (var child in this.Childrens)
                child.ProcessMouseMovedEvent(x, y);
        }

        public void ProcessMousePressedEvent(int x, int y, Mouse.Button button)
        {
            if (this.IsHover(x, y))
                this.MousePressed?.Invoke(this, button);
            foreach (var child in this.Childrens)
                if (child.IsHover(x, y))
                    child.ProcessMousePressedEvent(x, y, button);
        }

        public void ProcessMouseReleasedEvent(int x, int y, Mouse.Button button)
        {
            if (this.IsHover(x, y))
                this.MouseReleased?.Invoke(this, button);
            foreach (var child in this.Childrens)
                if (child.IsHover(x, y))
                    child.ProcessMouseReleasedEvent(x, y, button);
        }

        public virtual void Dispose()
        {
            foreach (var child in this.Childrens)
                child.Dispose();
        }
    }
}
