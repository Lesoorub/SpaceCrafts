using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicsFundation.Graphics.Forms;
using GraphicsFundation.Graphics.Forms.Layouts;
using SFML.Graphics;
using SFML.System;

namespace ClientApplication.Scenes
{
    partial class Menu
    {
        //menu
        // newGameBtn
        // loadGameBtn
        // settingBtn
        // quitBtn
        Rectangle menu;
        VerticalLayout menuLayout;
        Button newGameBtn;
        Button loadGameBtn;
        Button settingsBtn;
        Button quitBtn;
        DebugPanel debugPanel;
        public void InitComponents()
        {
            this.menu = new Rectangle(); 
            this.menu.Size = new Vector2f(200, 200);
            this.menu.FillColor = new Color(0x333333FF);
            this.menu.OutlineColor = Color.White;
            this.menu.OutlineThickness = 1;
            this.menu.Dock = Dock.VerticalMiddle;
            this.menu.Padding = new Padding(5, 5, 5, 5);
            this.menu.Parent = this;

            this.menuLayout = new VerticalLayout();
            this.menuLayout.Spacing = 5;
            this.menuLayout.ChildAligment = VerticalLayout.Aligment.Middle;
            this.menuLayout.Parent = this.menu;
            this.menuLayout.Dock = Dock.Fill;

            this.newGameBtn = new Button();
            this.newGameBtn.Label.Text = "New game";
            this.newGameBtn.Size = new Vector2f(0, 50);
            this.newGameBtn.Parent = this.menuLayout;

            this.loadGameBtn = new Button();
            this.loadGameBtn.Label.Text = "Load game";
            this.loadGameBtn.Size = new Vector2f(0, 50);
            this.loadGameBtn.Parent = this.menuLayout;

            this.settingsBtn = new Button();
            this.settingsBtn.Label.Text = "Settings";
            this.settingsBtn.Size = new Vector2f(0, 50);
            this.settingsBtn.Parent = this.menuLayout;

            this.quitBtn = new Button();
            this.quitBtn.Label.Text = "Quit";
            this.quitBtn.Size = new Vector2f(0, 50);
            this.quitBtn.Parent = this.menuLayout;

            this.debugPanel = new DebugPanel();
            this.debugPanel.GlobalPosition = new Vector2f(20, 20);
            this.debugPanel.Size = new Vector2f(300, 150);
            this.debugPanel.Parent = this;
        }
    }
}
