using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientApplication.Forms.UserControls;
using GraphicsFundation.Graphics.Forms;
using GraphicsFundation.Graphics.Forms.Layouts;
using SFML.Graphics;
using SFML.System;

namespace ClientApplication.Forms
{
    partial class Menu
    {
        //Form tree:
        //menu
        //-newGameBtn
        //-loadGameBtn
        //-settingBtn
        //-quitBtn
        //debugPanel
        Rectangle menu;
        VerticalLayout menuLayout;
        Button newGameBtn;
        Button loadGameBtn;
        Button settingsBtn;
        Button quitBtn;
        DebugPanel debugPanel;
        SubmitDialog quitSubmit;
        public void InitComponents()
        {
            this.menu = new Rectangle(); 
            this.menu.Parent = this;
            this.menu.Size = new Vector2f(200, 200);
            this.menu.FillColor = new Color(0x333333FF);
            this.menu.OutlineColor = Color.White;
            this.menu.OutlineThickness = 1;
            this.menu.Dock = Dock.VerticalMiddle;
            this.menu.Padding = new Padding(5, 5, 5, 5);

            this.menuLayout = new VerticalLayout();
            this.menuLayout.Parent = this.menu;
            this.menuLayout.Spacing = 5;
            this.menuLayout.ChildAligment = VerticalLayout.Aligment.Middle;
            this.menuLayout.Dock = Dock.Fill;

            this.newGameBtn = new Button();
            this.newGameBtn.Parent = this.menuLayout;
            this.newGameBtn.Label.Text = "New game";
            this.newGameBtn.Size = new Vector2f(0, 50);

            this.loadGameBtn = new Button();
            this.loadGameBtn.Parent = this.menuLayout;
            this.loadGameBtn.Label.Text = "Load game";
            this.loadGameBtn.Size = new Vector2f(0, 50);

            this.settingsBtn = new Button();
            this.settingsBtn.Parent = this.menuLayout;
            this.settingsBtn.Label.Text = "Settings";
            this.settingsBtn.Size = new Vector2f(0, 50);

            this.quitBtn = new Button();
            this.quitBtn.Parent = this.menuLayout;
            this.quitBtn.Label.Text = "Quit";
            this.quitBtn.Size = new Vector2f(0, 50);
            this.quitBtn.Click += this.QuitBtn_Click;

            this.debugPanel = new DebugPanel();
            this.debugPanel.Parent = this;
            this.debugPanel.GlobalPosition = new Vector2f(20, 20);
            this.debugPanel.Size = new Vector2f(300, 150);

            this.quitSubmit = new SubmitDialog();
            this.quitSubmit.Parent = this;
            this.quitSubmit.Visible = false;
            this.quitSubmit.Choised += this.QuitSubmit_Choised;
        }

        private void QuitSubmit_Choised(SubmitDialog.ChoiseResult result)
        {
            if (result == SubmitDialog.ChoiseResult.Yes)
                this.window.Close();
        }

        public override void Dispose()
        {
            this.quitBtn.Click -= this.QuitBtn_Click;
            base.Dispose();
        }

        private void QuitBtn_Click(object sender, SFML.Window.Mouse.Button e)
        {
            this.quitSubmit.Visible = true;
        }
    }
}
