using System.Diagnostics;
using ClientApplication.Graphics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GraphicsFundation.Graphics.Forms
{
    public class Form : Control
    {
        Stopwatch clock = new Stopwatch();
        public readonly ClientWindow window;

        Control? m_lastPressedControl;
        Mouse.Button? m_lastPressedMouseButton;

        Vector2i m_mousePosition;
        private Vector2f m_movingOffset;
        private Control? m_movingControl;

        public Vector2i MousePosition
        {
            get => this.m_mousePosition;
            private set => this.m_mousePosition = value + (Vector2i)this.LocalPosition;
        }

        public Form(ClientWindow window)
        {
            this.window = window;
            this.Size = (Vector2f)window.Size;
            this.window.OnResized += this.Window_OnResized;
            this.window.CameraPositionChanged += this.Window_CameraPositionChanged;
            this.window.MouseButtonPressed += this.Window_MouseButtonPressed;
            this.window.MouseButtonReleased += this.Window_MouseButtonReleased;
            this.window.MouseMoved += this.Window_MouseMoved;
        }

        private void Window_MouseButtonReleased(object? sender, MouseButtonEventArgs e)
        {
            var x = this.MousePosition.X;
            var y = this.MousePosition.Y;
            this.ProcessMouseReleasedEvent(x, y, e.Button);
            if (this.m_lastPressedMouseButton == e.Button &&
                this.m_lastPressedControl != null &&
                this.GetHovered(x, y) == this.m_lastPressedControl)
            {
                this.m_lastPressedControl.ProcessClick(e.Button);
            }
        }
        private void Window_MouseButtonPressed(object? sender, MouseButtonEventArgs e)
        {
            var x = this.MousePosition.X;
            var y = this.MousePosition.Y;
            this.ProcessMousePressedEvent(x, y, e.Button);
            this.m_lastPressedControl = this.GetHovered(x, y);
            this.m_lastPressedMouseButton = e.Button;
        }

        private void Window_MouseMoved(object? sender, MouseMoveEventArgs e)
        {
            this.MousePosition = new Vector2i(e.X, e.Y);
            this.ProcessMouseMovedEvent(this.MousePosition.X, this.MousePosition.Y);
            if (this.m_movingControl != null)
                this.MovingUpdate();
        }


        private void Window_CameraPositionChanged(Vector2f obj)
        {
            this.CorrectFormPosition();
        }

        private void Window_OnResized(object? sender, SFML.Window.SizeEventArgs e)
        {
            this.Size = (Vector2f)this.window.Size;
            this.CorrectFormPosition();
        }

        private void CorrectFormPosition()
        {
            this.LocalPosition = this.window.CameraPosition - this.Size / 2;
        }

        public override void Dispose()
        {
            this.window.OnResized -= this.Window_OnResized;
            this.window.CameraPositionChanged -= this.Window_CameraPositionChanged;
            this.window.MouseButtonReleased -= this.Window_MouseButtonReleased;
            this.window.MouseButtonPressed -= this.Window_MouseButtonPressed;
            this.window.MouseMoved -= this.Window_MouseMoved;
            base.Dispose();
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            float deltaTime = (float)this.clock.ElapsedTicks / TimeSpan.TicksPerMillisecond;
            this.clock.Restart();
            this.Update(deltaTime);
            base.Update(deltaTime);
            base.Draw(target, states);
        }
        public void BeginMoving(Control movingControl)
        {
            this.m_movingControl = movingControl;
            this.m_movingOffset = this.m_movingControl.GlobalPosition - (Vector2f)this.m_mousePosition;
        }

        public void MovingUpdate()
        {
            if (this.m_movingControl == null) return;
            this.m_movingControl.GlobalPosition = (Vector2f)this.m_mousePosition + this.m_movingOffset;
        }

        public void EndMoving()
        {
            this.m_movingControl = null;
        }

    }
}
