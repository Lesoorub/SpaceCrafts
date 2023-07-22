using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicsFundation.Graphics.Forms.Layouts;
using SFML.Graphics;
using SFML.System;

namespace GraphicsFundation.Graphics.Forms
{
    public class DebugPanel : Control
    {
        public Rectangle Window;
        public Rectangle Header;
        public Label HeaderLabel;
        public VerticalLayout BodyLayout;
        public Button CollapseBtn;

        public Label TypeLabel;
        public Label ChildCountLabel;

        public VertexArray boundsView;
        public VertexArray sizeView;

        public Color BackgroundColor = new Color(0x151617FF);
        public Color OutlineColor = new Color(0x4c5c7dFF);
        public Color HeaderColor = new Color(0x294a7aFF);

        public bool ShowSize = true;
        public bool ShowBoudns = true;

        public DebugPanel()
        {
            this.Window = new Rectangle();
            this.Window.Parent = this;
            this.Window.FillColor = this.BackgroundColor;
            this.Window.OutlineColor = this.OutlineColor;
            this.Window.OutlineThickness = 1;
            this.Window.Dock = Dock.Fill;
            this.IsHoverable = false;

            this.Header = new Rectangle();
            this.Header.Parent = this.Window;
            this.Header.FillColor = this.HeaderColor;
            this.Header.OutlineColor = this.OutlineColor;
            this.Header.OutlineThickness = 1;
            this.Header.Size = new Vector2f(x: 0, y: 20);
            this.Header.Dock = Dock.HorizontalTop;

            this.HeaderLabel = new Label();
            this.HeaderLabel.Parent = this.Header;
            this.HeaderLabel.Vertical = Label.VerticalAligment.Middle;
            this.HeaderLabel.Horizontal = Label.HorizontalAligment.Left;
            this.HeaderLabel.Text = "Debug panel";
            this.HeaderLabel.Dock = Dock.Fill;
            this.HeaderLabel.FontSize = 15;
            this.HeaderLabel.Margin = new Padding(left: 3, top: 3, right: 0, bottom: 0);

            this.CollapseBtn = new Button();
            this.CollapseBtn.Parent = this.Header;
            this.CollapseBtn.Label.Text = "_";
            this.CollapseBtn.Size = new Vector2f(this.Header.Size.Y, this.Header.Size.Y);
            this.CollapseBtn.NormalBackColor = Color.Transparent;
            this.CollapseBtn.HoveredBackColor = new Color(0xFFFFFF33);
            this.CollapseBtn.PressedBackColor = Color.White;
            this.CollapseBtn.PressedForeColor = Color.Black;
            this.CollapseBtn.NormalBackColor = Color.Transparent;
            this.CollapseBtn.Background.OutlineThickness = 0;
            this.CollapseBtn.UpdateColors();
            this.CollapseBtn.Dock = Dock.VerticalRight;
            this.CollapseBtn.Click += this.CollapseBtn_Click;

            this.BodyLayout = new VerticalLayout();
            this.BodyLayout.Parent = this.Window;
            this.BodyLayout.Spacing = 5;
            this.BodyLayout.ChildAligment = VerticalLayout.Aligment.Top;
            this.BodyLayout.Dock = Dock.Fill;
            this.BodyLayout.Padding = new Padding(left: 3, top: 23, right: 0, bottom: 0);

            this.TypeLabel = new Label();
            this.TypeLabel.Parent = this.BodyLayout;
            this.TypeLabel.FontSize = 14;
            this.TypeLabel.Vertical = Label.VerticalAligment.Top;
            this.TypeLabel.Size = new Vector2f(this.BodyLayout.Size.X - this.BodyLayout.Padding.Right - this.BodyLayout.Padding.Left, 15);

            this.ChildCountLabel = new Label();
            this.ChildCountLabel.Parent = this.BodyLayout;
            this.ChildCountLabel.FontSize = 14;
            this.ChildCountLabel.Vertical = Label.VerticalAligment.Top;
            this.ChildCountLabel.Size = new Vector2f(this.BodyLayout.Size.X - this.BodyLayout.Padding.Right - this.BodyLayout.Padding.Left, 15);

            this.boundsView = new VertexArray(PrimitiveType.LineStrip, 5);
            this.sizeView = new VertexArray(PrimitiveType.LineStrip, 5);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            if (this.Root is Form form)
            {
                var mpos = form.MousePosition;
                var hovered = form.GetHovered(mpos.X, mpos.Y);
                if (hovered != null)
                {
                    this.UpdateInfo(hovered);
                }
            }
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
            if (this.ShowSize)
                this.sizeView.Draw(target, states);
            if (this.ShowBoudns)
                this.boundsView.Draw(target, states);
        }

        public void UpdateInfo(Control target) 
        {
            this.TypeLabel.Text = $"Type: {target.GetType().Name}";
            var c = target.Controls;
            this.ChildCountLabel.Text = $"Childrens.Length: {c.Length}\n {string.Join("\n ", c.Select(x => x.GetType().Name))}".TrimEnd();

            if (this.ShowBoudns)
                this.UpdateBounds(target);
            if (this.ShowSize)
                this.UpdateSize(target);
        }

        private void UpdateBounds(Control target)
        {
            var b = target.Bounds;
            var vertex = this.boundsView[index: 0];
            vertex.Color = Color.Magenta;
            vertex.Position = new Vector2f(b.Left, b.Top);
            this.boundsView[index: 0] = vertex;
            vertex.Position = new Vector2f(b.Left + b.Width, b.Top);
            this.boundsView[index: 1] = vertex;
            vertex.Position = new Vector2f(b.Left + b.Width, b.Top + b.Height);
            this.boundsView[index: 2] = vertex;
            vertex.Position = new Vector2f(b.Left, b.Top + b.Height);
            this.boundsView[index: 3] = vertex;
            vertex.Position = new Vector2f(b.Left, b.Top);
            this.boundsView[index: 4] = vertex;
        }
        private void UpdateSize(Control target)
        {
            var g = target.GlobalPosition;
            var s = target.Size;
            var b = new FloatRect(g.X, g.Y, s.X, s.Y);
            var vertex = this.sizeView[index: 0];
            vertex.Color = Color.Cyan;
            vertex.Position = new Vector2f(b.Left, b.Top);
            this.sizeView[index: 0] = vertex;
            vertex.Position = new Vector2f(b.Left + b.Width, b.Top);
            this.sizeView[index: 1] = vertex;
            vertex.Position = new Vector2f(b.Left + b.Width, b.Top + b.Height);
            this.sizeView[index: 2] = vertex;
            vertex.Position = new Vector2f(b.Left, b.Top + b.Height);
            this.sizeView[index: 3] = vertex;
            vertex.Position = new Vector2f(b.Left, b.Top);
            this.sizeView[index: 4] = vertex;
        }

        private void CollapseBtn_Click(object? sender, SFML.Window.Mouse.Button e)
        {

        }
    }
}
