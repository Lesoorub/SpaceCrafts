﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicsFundation.Graphics.Forms.Layouts;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GraphicsFundation.Graphics.Forms
{
    public class DebugPanel : Control
    {
        public Rectangle Body;
        public Rectangle Header;
        public Label HeaderLabel;
        public VerticalLayout BodyLayout;
        public Button CollapseBtn;

        public Button ToggleBoundView;
        public Button ToggleSizeView;

        public Label TypeLabel;
        public Label ChildCountLabel;

        private VertexArray boundsView;
        private VertexArray sizeView;

        public Color BackgroundColor = new Color(0x151617FF);
        public Color OutlineColor = new Color(0x4c5c7dFF);
        public Color HeaderColor = new Color(0x294a7aFF);

        public bool ShowSize = false;
        public bool ShowBoudns = false;

        public DebugPanel()
        {
            //
            // Header
            //
            this.Header = new Rectangle();
            this.Header.Parent = this;
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
            this.HeaderLabel.FontSize = 14;
            this.HeaderLabel.Margin = new Padding(left: 3, top: 3, right: 0, bottom: 0);

            this.CollapseBtn = new Button();
            this.CollapseBtn.Parent = this.Header;
            this.CollapseBtn.Label.FontSize = 14;
            this.CollapseBtn.Label.Text = "_";
            this.CollapseBtn.Size = new Vector2f(this.Header.Size.Y, this.Header.Size.Y);
            this.CollapseBtn.NormalBackColor = Color.Transparent;
            this.CollapseBtn.HoveredBackColor = new Color(0xFFFFFF33);
            this.CollapseBtn.PressedBackColor = Color.White;
            this.CollapseBtn.PressedForeColor = Color.Black;
            this.CollapseBtn.NormalBackColor = Color.Transparent;
            this.CollapseBtn.Background.OutlineThickness = 0;
            this.CollapseBtn.SetState(Button.State.Normal);
            this.CollapseBtn.Dock = Dock.VerticalRight;
            this.CollapseBtn.Click += this.CollapseBtn_Click;

            //
            // Body
            //
            this.Body = new Rectangle();
            this.Body.Parent = this;
            this.Body.Margin = new Padding(left: 0, top: 20, right: 0, bottom: 0);
            this.Body.FillColor = this.BackgroundColor;
            this.Body.OutlineColor = this.OutlineColor;
            this.Body.OutlineThickness = 1;
            this.Body.Dock = Dock.Fill;

            this.BodyLayout = new VerticalLayout();
            this.BodyLayout.Parent = this.Body;
            this.BodyLayout.Spacing = 5;
            this.BodyLayout.ChildAligment = VerticalLayout.Aligment.Top;
            this.BodyLayout.Dock = Dock.Fill;
            this.BodyLayout.Padding = new Padding(left: 3, top: 3, right: 3, bottom: 3);

            this.ToggleBoundView = new Button();
            this.ToggleBoundView.Parent = this.BodyLayout;
            this.ToggleBoundView.Size = new Vector2f(0, 15);
            this.ToggleBoundView.Label.FontSize = 14;
            this.ToggleBoundView.Label.Text = "Bound";
            this.ToggleBoundView.Click += this.ToggleBoundView_Click;
            this.ToggleBoundView.SubtractBaseHandlers();

            this.ToggleSizeView = new Button();
            this.ToggleSizeView.Parent = this.BodyLayout;
            this.ToggleSizeView.Size = new Vector2f(0, 15);
            this.ToggleSizeView.Label.FontSize = 14;
            this.ToggleSizeView.Label.Text = "Size";
            this.ToggleSizeView.Click += this.ToggleSizeView_Click;
            this.ToggleSizeView.SubtractBaseHandlers();

            this.TypeLabel = new Label();
            this.TypeLabel.Parent = this.BodyLayout;
            this.TypeLabel.FontSize = 14;
            this.TypeLabel.Vertical = Label.VerticalAligment.Top;
            this.TypeLabel.Size = new Vector2f(0, 15);

            this.ChildCountLabel = new Label();
            this.ChildCountLabel.Parent = this.BodyLayout;
            this.ChildCountLabel.FontSize = 14;
            this.ChildCountLabel.Vertical = Label.VerticalAligment.Top;
            this.ChildCountLabel.Size = new Vector2f(0, 15);

            this.boundsView = new VertexArray(PrimitiveType.LineStrip, 5);
            this.sizeView = new VertexArray(PrimitiveType.LineStrip, 5);

        }

        private void ToggleSizeView_Click(object? sender, Mouse.Button e)
        {
            this.ShowSize = !this.ShowSize;
            this.ToggleSizeView.SetState(this.ShowSize ? Button.State.Pressed : Button.State.Normal);
        }

        private void ToggleBoundView_Click(object? sender, Mouse.Button e)
        {
            this.ShowBoudns = !this.ShowBoudns;
            this.ToggleBoundView.SetState(this.ShowBoudns ? Button.State.Pressed : Button.State.Normal);
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
            this.UpdateVertexArrayByRect(this.boundsView, Color.Magenta, b);
        }
        private void UpdateSize(Control target)
        {
            var g = target.GlobalPosition;
            var s = target.Size;
            var b = new FloatRect(g.X, g.Y, s.X, s.Y);
            this.UpdateVertexArrayByRect(this.sizeView, Color.Cyan, b);
        }

        private void UpdateVertexArrayByRect(VertexArray array, Color color, FloatRect rect)
        {
            var vertex = array[index: 0];
            vertex.Color = color;
            vertex.Position = new Vector2f(rect.Left, rect.Top);
            array[index: 0] = vertex;
            vertex.Position = new Vector2f(rect.Left + rect.Width, rect.Top);
            array[index: 1] = vertex;
            vertex.Position = new Vector2f(rect.Left + rect.Width, rect.Top + rect.Height);
            array[index: 2] = vertex;
            vertex.Position = new Vector2f(rect.Left, rect.Top + rect.Height);
            array[index: 3] = vertex;
            vertex.Position = new Vector2f(rect.Left, rect.Top);
            array[index: 4] = vertex;
        }

        private void CollapseBtn_Click(object? sender, Mouse.Button e)
        {
            this.Body.Visible = !this.Body.Visible;
            this.CollapseBtn.Label.Text = this.Body.Visible ? "_" : "#";
        }

        public override void Dispose()
        {
            this.ToggleBoundView.Click -= this.ToggleBoundView_Click;
            this.ToggleSizeView.Click -= this.ToggleSizeView_Click;
            base.Dispose();
        }
    }
}