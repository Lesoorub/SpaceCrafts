using BuilderGUI.Controls;
using BuilderGUI.Scenes;
using SFML.Graphics;
using Color = SFML.Graphics.Color;

namespace BuilderGUI
{
    public partial class Form1 : Form
    {
        RenderWindow? window;
        Scene scene;
        public Form1()
        {
            this.InitializeComponent();
            this.scene = new TestScene();
        }

        private void addElement_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.window = new RenderWindow(this.pictureBox1.Handle);
            this.window.MouseButtonPressed += this.Window_MouseButtonPressed;
            this.window.MouseButtonReleased += this.Window_MouseButtonReleased;
        }

        private void Window_MouseButtonReleased(object? sender, SFML.Window.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Window_MouseButtonPressed(object? sender, SFML.Window.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.window == null) return;
            this.window.Clear(Color.White);
            this.window.DispatchEvents();
            this.scene.Draw(this.window);

            var scene = this.scene as TestScene;

            foreach (var rect in scene!.label1!.GetWordRectangles())
            {
                SFMLPrimitives.DrawRectangle(this.window, new SFMLPrimitives.RectangleDrawData()
                {
                    Position = scene.label1!.GlobalPostion + new SFML.System.Vector2f(rect.Left, rect.Top),
                    Size = new SFML.System.Vector2f(rect.Width, rect.Height),
                    BorderTickness = 1,
                    BorderColor = Color.Green,
                });
            }


            this.window.Display();
        }
    }
}