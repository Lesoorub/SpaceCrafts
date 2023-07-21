using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace ClientApplication.Graphics
{
    public class ClientWindow : IDisposable
    {
        #region Fields
        
        RenderWindow window;
        public Drawable? Drawable;

        #endregion

        #region Properties

        public Vector2u Size => this.window.Size;

        #endregion

        #region Events

        public event Action? OnClosing;
        public event Action? OnClosed;
        public event EventHandler<SizeEventArgs>? OnResized;

        #endregion

        #region Constructors

        public ClientWindow()
        {
            this.window = GraphicSettings.Default.CreateRendererWindow();
            this.Setup();
        }

        #endregion

        #region Methods

        #region Public

        /// <summary>
        /// Запускает цикл отрисовки окна, блокируя поток
        /// </summary>
        public void Run()
        {
            while (this.window.IsOpen)
            {
                this.window.Clear(Color.Black);
                this.window.DispatchEvents();
                if (this.Drawable != null)
                    this.window.Draw(this.Drawable);
                this.window.Display();
            }
        }

        public void Dispose()
        {
            this.window.Dispose();
        }

        #endregion

        #region Private

        void Setup()
        {
            this.window.Closed += this.Window_Closed;
            this.window.Resized += this.Window_Resized;
        }

        private void Window_Resized(object? sender, SizeEventArgs e)
        {
            this.window.Size = new Vector2u(e.Width, e.Height);
            this.OnResized?.Invoke(sender, e);
        }

        #endregion

        #region Handlers

        private void Window_Closed(object? sender, EventArgs e)
        {
            this.OnClosing?.Invoke();
            this.window.Close();
            this.OnClosed?.Invoke();
        }

        #endregion

        #endregion
    }
}
