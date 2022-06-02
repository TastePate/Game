using Game.Model;
using Game.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Control
{
    public partial class DeadControl : UserControl
    {
        private GameEntity game;
        private TableLayoutPanel buttonTable;
        private Button winButton;

        public DeadControl()
        {
            InitializeComponent();
            Size = new Size(1920, 1080);
            BackgroundImage = new Bitmap(Folders.Background[0]);
            InitializeWinButton();
        }

        public void Configure(GameEntity game)
        {
            if (this.game != null)
                return;

            this.game = game;
        }

        public void InitializeWinButton()
        {
            buttonTable = new TableLayoutPanel
            {
                Location = new Point(660, 300),
                Size = new Size(210, 270),
            };
            winButton = new Button()
            {
                Image = new Bitmap(Folders.Buttons
                                          .Where(x => x == "C:\\Users\\TastePate\\OneDrive - УрФУ\\Рабочий стол\\Game\\Game\\bin\\Debug\\..\\..\\View\\Buttons\\GameOverButton.png")
                                          .FirstOrDefault()),
                Dock = DockStyle.Fill
            };
            winButton.FlatAppearance.BorderSize = 0;
            winButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            winButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            winButton.Click += (s, e) => Application.Exit();
            buttonTable.Controls.Add(winButton);
            buttonTable.Controls.Add(winButton, 0, 0);
            Controls.Add(buttonTable);
        }
    }
}
