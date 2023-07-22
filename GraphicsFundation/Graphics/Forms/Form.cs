using System.Diagnostics;
using ClientApplication.Graphics;
using SFML.Graphics;
using SFML.System;

namespace GraphicsFundation.Graphics.Forms
{
    public class Form : Control
    {
        Stopwatch clock = new Stopwatch();
        public readonly ClientWindow window;

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

        private void Window_MouseButtonReleased(object? sender, SFML.Window.MouseButtonEventArgs e)
        {
            this.ProcessMouseReleasedEvent(e.X + (int)this.LocalPosition.X, e.Y + (int)this.LocalPosition.Y, e.Button);
        }

        private void Window_MouseMoved(object? sender, SFML.Window.MouseMoveEventArgs e)
        {
            this.ProcessMouseMovedEvent(e.X + (int)this.LocalPosition.X, e.Y + (int)this.LocalPosition.Y);
        }

        private void Window_MouseButtonPressed(object? sender, SFML.Window.MouseButtonEventArgs e)
        {
            this.ProcessMousePressedEvent(e.X + (int)this.LocalPosition.X, e.Y + (int)this.LocalPosition.Y, e.Button);
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
    }
}
