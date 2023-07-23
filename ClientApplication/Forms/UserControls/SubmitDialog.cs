using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicsFundation.Graphics.Forms;
using GraphicsFundation.Graphics.Forms.Layouts;
using SFML.Graphics;
using SFML.System;

namespace ClientApplication.Forms.UserControls
{
    internal class SubmitDialog : Control
    {
        Rectangle headerRect;
        Label headerTitleLabel;
        Button cancelBtn;
        Rectangle backgroundRect;
        Label bodyText;
        HorizontalSplit bodyBtnsSplit;
        Button yesBtn;
        Button noBtn;

        public string TitleText
        {
            get => this.headerTitleLabel.Text;
            set => this.headerTitleLabel.Text = value;
        }

        public string BodyText
        {
            get => this.bodyText.Text;
            set => this.bodyText.Text = value;
        }

        public event Action<ChoiseResult>? Choised;

        public SubmitDialog()
        {
            this.Size = new Vector2f(300, 150);
            this.Dock = Dock.Middle;
            //
            // Header
            //
            this.headerRect = new Rectangle()
            {
                Parent = this,
                Size = new Vector2f(x: 0, y: 20),
                Padding = new Padding(left: 3, top: 0, right: 0, bottom: 0),
                Dock = Dock.HorizontalTop,
                FillColor = Colors.ColoredBack,
                OutlineThickness = 1,
                OutlineColor = Colors.Outline,
            };

            this.cancelBtn = new Button()
            {
                Parent = this.headerRect,
                Size = new Vector2f(this.headerRect.Size.Y, this.headerRect.Size.Y),
                Dock = Dock.VerticalRight,
            };
            this.cancelBtn.Background.OutlineThickness = 0;
            this.cancelBtn.NormalBackColor = Color.Transparent;
            this.cancelBtn.NormalForeColor = Color.White;
            this.cancelBtn.HoveredBackColor = new Color(0xFFFFFF33);
            this.cancelBtn.SetState(Button.State.Normal);
            this.cancelBtn.Label.FontSize = 14;
            this.cancelBtn.Label.Text = "X";
            this.cancelBtn.Click += this.CancelBtn_Click;

            this.headerTitleLabel = new Label()
            {
                Parent = this.headerRect,
                FontSize = 14,
                Text = "Sumbit dialog",
                Horizontal = Label.HorizontalAligment.Left,
                Vertical = Label.VerticalAligment.Middle,
                Dock = Dock.Fill,
                Margin = new Padding(left: 0, top: 0, right: this.cancelBtn.Size.X, bottom: 0),
            };

            //
            // Body
            //
            this.backgroundRect = new Rectangle()
            {
                Parent = this,
                Margin = new Padding(left: 0, top: this.headerRect.Size.Y, right: 0, bottom: 0),
                Dock = Dock.Fill,
                FillColor = Colors.Background,
                OutlineThickness = 1,
                OutlineColor = Colors.Outline,
            };

            this.bodyBtnsSplit = new HorizontalSplit()
            {
                Parent = this.backgroundRect,
                Size = new Vector2f(0, 25),
                Dock = Dock.HorizontalBottom,
                Padding = new Padding(5, 5, 5, 5),
                Spacing = 5
            };

            this.bodyText = new Label()
            {
                Parent = this.backgroundRect,
                Margin = new Padding(left: 0, top: 0, right: 0, bottom: this.bodyBtnsSplit.Size.Y),
                Dock = Dock.Fill,
                FontSize = 14,
                Horizontal = Label.HorizontalAligment.Middle,
                Vertical = Label.VerticalAligment.Middle,
                Text = "Do you confirm your choice?",
            };

            this.yesBtn = new Button()
            {
                Parent = this.bodyBtnsSplit,
            };
            this.yesBtn.Label.FontSize = 14;
            this.yesBtn.Label.Text = "Yes";
            this.yesBtn.Click += this.YesBtn_Click;

            this.noBtn = new Button()
            {
                Parent = this.bodyBtnsSplit,
            };
            this.noBtn.Label.FontSize = 14;
            this.noBtn.Label.Text = "No";
            this.noBtn.Click += this.NoBtn_Click;
        }

        private void NoBtn_Click(object? sender, SFML.Window.Mouse.Button e)
        {
            this.Choised?.Invoke(ChoiseResult.No);
            this.Visible = false;
        }

        private void YesBtn_Click(object? sender, SFML.Window.Mouse.Button e)
        {
            this.Choised?.Invoke(ChoiseResult.Yes);
            this.Visible = false;
        }

        private void CancelBtn_Click(object? sender, SFML.Window.Mouse.Button e)
        {
            this.Choised?.Invoke(ChoiseResult.Cancel);
            this.Visible = false;
        }

        public override void Dispose()
        {
            this.noBtn.Click -= this.NoBtn_Click;
            this.yesBtn.Click -= this.YesBtn_Click;
            this.cancelBtn.Click -= this.CancelBtn_Click;
            base.Dispose();
        }

        public enum ChoiseResult : byte
        {
            Cancel,
            Yes,
            No
        }
    }
}
