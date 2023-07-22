using SFML.Graphics;
using static GraphicsFundation.Graphics.Forms.Button;

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
            this.SetState(State.Normal);
            this.Background.OutlineThickness = 1;
            this.Background.IsHoverable = false;
            this.AddChild(this.Background);

            this.Label.Horizontal = Label.HorizontalAligment.Middle;
            this.Label.Vertical = Label.VerticalAligment.Middle;
            this.Label.IsHoverable = false;
            this.AddChild(this.Label);

            this.Background.Dock = Dock.Fill;
            this.Label.Dock = Dock.Fill;

            this.AddBaseHandlers();
        }

        public void SetState(State newState)
        {
            this.Background.OutlineColor = this.NormalForeColor;
            switch (newState)
            {
                case State.Normal:
                    this.Background.FillColor = this.NormalBackColor;
                    this.Label.ForeColor = this.NormalForeColor;
                    break;
                case State.Hovered:
                    this.Background.FillColor = this.HoveredBackColor;
                    this.Label.ForeColor = this.HoveredForeColor;
                    break;
                case State.Pressed:
                    this.Background.FillColor = this.PressedBackColor;
                    this.Label.ForeColor = this.PressedForeColor;
                    break;
            }
        }

        public void AddBaseHandlers()
        {
            this.MouseEnter += this.OnMouseEnter;
            this.MouseLeave += this.OnMouseLeave;
            this.MousePressed += this.OnMousePressed;
            this.MouseReleased += this.OnMouseReleased;
        }
        public void SubtractBaseHandlers()
        {
            this.MouseEnter -= this.OnMouseEnter;
            this.MouseLeave -= this.OnMouseLeave;
            this.MousePressed -= this.OnMousePressed;
            this.MouseReleased -= this.OnMouseReleased;
        }

        private void OnMouseEnter(object? sender, EventArgs e)
        {
            this.SetState(State.Hovered);
        }
        private void OnMouseLeave(object? sender, EventArgs e)
        {
            this.SetState(State.Normal);
        }
        private void OnMousePressed(object? sender, SFML.Window.Mouse.Button button)
        {
            this.SetState(State.Pressed);
        }
        private void OnMouseReleased(object? sender, SFML.Window.Mouse.Button button)
        {
            this.SetState(State.Normal);
        }

        public override void Dispose()
        {
            this.SubtractBaseHandlers();

            base.Dispose();
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
        }

        public enum State
        {
            Normal = 0,
            Hovered,
            Pressed
        }
    }
}
