﻿using System;
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
        public event EventHandler<Mouse.Button>? MousePressed;
        public event EventHandler<Mouse.Button>? MouseReleased;
        public event EventHandler<Mouse.Button>? Click;
        public event EventHandler? MouseMoved;
        public event EventHandler? MouseEnter;
        public event EventHandler? MouseLeave;

        public bool Visible { get; set; } = true;

        public bool IsMouseHovered { get; private set; }

        public Dock Dock
        {
            get => this.m_dock;
            set
            {
                this.m_dock = value;
                var p_size = new Vector2f(0, 0);
                var margin = this.Margin;
                var p_zero = new Vector2f(0, 0);
                if (this.m_parent != null)
                {
                    var p_padding = this.m_parent.Padding;
                    p_zero = new Vector2f(
                            p_padding.Left + margin.Left,
                            p_padding.Top + margin.Top);
                    p_size = this.m_parent.Size - new Vector2f(
                                p_padding.Right + margin.Right + p_zero.X,
                                p_padding.Bottom + margin.Bottom + p_zero.Y);
                }

                switch (this.m_dock)
                {
                    case Dock.None:
                        break;
                    case Dock.HorizontalUnclipped:
                        if (this.m_parent != null)
                            this.m_size.X = p_size.X;
                        this.m_position.X = p_zero.X;
                        break;
                    case Dock.VerticallUnclipped:
                        if (this.m_parent != null)
                            this.m_size.Y = p_size.Y;
                        this.m_position.Y = p_zero.Y;
                        break;
                    case Dock.Fill:
                        if (this.m_parent != null)
                            this.m_size = p_size;
                        this.m_position = p_zero;
                        break;
                    case Dock.HorizontalMiddle:
                        this.m_position.X = p_zero.X;
                        if (this.m_parent != null)
                        {
                            this.m_size.X = p_size.X;
                            this.m_position.Y = p_size.Y / 2 - this.m_size.Y / 2 + p_zero.Y;
                        }
                        break;
                    case Dock.VerticalMiddle:
                        this.m_position.Y = p_zero.Y;
                        if (this.m_parent != null)
                        {
                            this.m_size.Y = p_size.Y;
                            this.m_position.X = p_size.X / 2 - this.m_size.X / 2 + p_zero.X;
                        }
                        break;
                    case Dock.HorizontalTop:
                        this.m_position = p_zero;
                        if (this.m_parent != null)
                            this.m_size.X = p_size.X;
                        break;
                    case Dock.VerticalLeft:
                        this.m_position = p_zero;
                        if (this.m_parent != null)
                            this.m_size.Y = p_size.Y;
                        break;
                    case Dock.HorizontalBottom:
                        this.m_position.X = p_zero.X;
                        if (this.m_parent != null)
                        {
                            this.m_size.X = p_size.X;
                            this.m_position.Y = p_size.Y - this.m_size.Y + p_zero.Y;
                        }
                        break;
                    case Dock.VerticalRight:
                        this.m_position.Y = p_zero.Y;
                        if (this.m_parent != null)
                        {
                            this.m_size.Y = p_size.Y;
                            this.m_position.X = p_size.X - this.m_size.X + p_zero.X;
                        }
                        break;
                    case Dock.Middle:
                        if (this.m_parent != null)
                            this.m_position = p_size / 2 - this.m_size / 2 + p_zero;
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

        public virtual Padding Padding { get; set; }

        public virtual Padding Margin { get; set; }

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

        public virtual bool IsHoverable { get; set; } = true;

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
            if (!this.Visible) return null;
            for (int i = this.Childrens.Count - 1; i >= 0; i--)
            {
                var child = this.Childrens[index: i];
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
                if (child.Visible)
                    child.Draw(target, states);
        }

        public virtual void ParentChanged(Control who)
        {
            foreach (var child in this.Childrens)
                child.ParentChanged(who);
        }

        private void Control_ParentChanged()
        {
            this.Dock = this.m_dock;
            this.Changed?.Invoke();
            this.ParentChanged(this);
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

        public void ProcessClick(Mouse.Button button)
        {
            this.Click?.Invoke(this, button);
        }

        public virtual void Dispose()
        {
            foreach (var child in this.Childrens)
                child.Dispose();
        }
    }
}
