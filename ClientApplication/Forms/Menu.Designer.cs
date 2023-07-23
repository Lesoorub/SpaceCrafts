using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientApplication.Core.Scene;
using ClientApplication.Forms.UserControls;
using ClientApplication.Scenes;
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
        Button singleplayer;
        Button multiplayer;
        Button settingsBtn;
        Button quitBtn;
        SubmitDialog quitSubmit;
        SubmitDialog multiplayerDialog;
        DebugPanel debugPanel;
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

            this.singleplayer = new Button();
            this.singleplayer.Parent = this.menuLayout;
            this.singleplayer.Label.FontSize = 24;
            this.singleplayer.Label.Text = "Singleplayer";
            this.singleplayer.Size = new Vector2f(0, 50);
            this.singleplayer.Click += this.Singleplayer_Click;

            this.multiplayer = new Button();
            this.multiplayer.Parent = this.menuLayout;
            this.multiplayer.Label.FontSize = 24;
            this.multiplayer.Label.Text = "Multiplayer";
            this.multiplayer.Size = new Vector2f(0, 50);
            this.multiplayer.Click += this.Multiplayer_Click;

            this.settingsBtn = new Button();
            this.settingsBtn.Parent = this.menuLayout;
            this.settingsBtn.Label.FontSize = 24;
            this.settingsBtn.Label.Text = "Settings";
            this.settingsBtn.Size = new Vector2f(0, 50);

            this.quitBtn = new Button();
            this.quitBtn.Parent = this.menuLayout;
            this.quitBtn.Label.FontSize = 24;
            this.quitBtn.Label.Text = "Quit";
            this.quitBtn.Size = new Vector2f(0, 50);
            this.quitBtn.Click += this.QuitBtn_Click;

            this.quitSubmit = new SubmitDialog();
            this.quitSubmit.Parent = this;
            this.quitSubmit.Visible = false;
            this.quitSubmit.Choised += this.QuitSubmit_Choised;

            this.multiplayerDialog = new SubmitDialog();
            this.multiplayerDialog.TitleText = "Упс!";
            this.multiplayerDialog.BodyText = "Временно не работает";
            this.multiplayerDialog.Parent = this;
            this.multiplayerDialog.Choised += this.MultiplayerDialog_Choised;
            this.multiplayerDialog.Visible = false;

            this.debugPanel = new DebugPanel();
            this.debugPanel.Parent = this;
            this.debugPanel.GlobalPosition = new Vector2f(20, 20);
            this.debugPanel.Size = new Vector2f(300, 150);
        }

        private void MultiplayerDialog_Choised(SubmitDialog.ChoiseResult obj)
        {
            this.multiplayerDialog.Visible = false;
        }

        private void Multiplayer_Click(object sender, SFML.Window.Mouse.Button e)
        {
            this.multiplayerDialog.Visible = true;
        }

        private void Singleplayer_Click(object sender, SFML.Window.Mouse.Button e)
        {
            SceneManager.SetScene(new Space());
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
