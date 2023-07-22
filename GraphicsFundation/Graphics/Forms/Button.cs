using SFML.Graphics;

namespace GraphicsFundation.Graphics.Forms
{
    public class Button : Control
    {
        public readonly Rectangle Background = new Rectangle();
        public readonly Label Label = new Label();

        public Color HoveredBackColor = new Color(0x333333FF);
        public Color NormalBackColor = new Color(0x222222FF);
        public Color PressedBackColor = new Color(0xFFFFFFFF);

        public Color HoveredForeColor = new Color(0xFFFFFFFF);
        public Color NormalForeColor = new Color(0xFFFFFFFF);
        public Color PressedForeColor = new Color(0x000000FF);

        public Button()
        {
            this.UpdateColors();
            this.Background.OutlineThickness = 1;
            this.Background.IsHoverable = false;
            this.AddChild(this.Background);

            this.Label.Horizontal = Label.HorizontalAligment.Middle;
            this.Label.Vertical = Label.VerticalAligment.Middle;
            this.Label.IsHoverable = false;
            this.AddChild(this.Label);

            this.Background.Dock = Dock.Fill;
            this.Label.Dock = Dock.Fill;

            this.MouseEnter += this.OnMouseEnter;
            this.MouseLeave += this.OnMouseLeave;
            this.MousePressed += this.OnMousePressed;
            this.MouseReleased += this.OnMouseReleased;
        }

        public void UpdateColors()
        {
            this.Background.FillColor = this.NormalBackColor;
            this.Background.OutlineColor = this.NormalForeColor;

            this.Label.ForeColor = this.NormalForeColor;
        }

        private void OnMouseEnter(object? sender, EventArgs e)
        {
            this.Background.FillColor = this.HoveredBackColor;
            this.Label.ForeColor = this.HoveredForeColor;
        }
        private void OnMouseLeave(object? sender, EventArgs e)
        {
            this.Background.FillColor = this.NormalBackColor;
            this.Label.ForeColor = this.NormalForeColor;
        }
        private void OnMousePressed(object? sender, SFML.Window.Mouse.Button button)
        {
            this.Background.FillColor = this.PressedBackColor;
            this.Label.ForeColor = this.PressedForeColor;
        }
        private void OnMouseReleased(object? sender, SFML.Window.Mouse.Button button)
        {
            this.Background.FillColor = this.HoveredBackColor;
            this.Label.ForeColor = this.HoveredForeColor;
        }

        public override void Dispose()
        {
            this.MouseEnter -= this.OnMouseEnter;
            this.MouseLeave -= this.OnMouseLeave;
            this.MousePressed -= this.OnMousePressed;
            this.MouseReleased -= this.OnMouseReleased;

            base.Dispose();
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
        }
    }
}
