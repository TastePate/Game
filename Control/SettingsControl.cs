using Game.Model;
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
    public partial class SettingsControl : UserControl
    {
        private Button Back;
        public GameEntity game;

        public SettingsControl()
        {
            InitializeComponent();
            Back = new Button() { Text = "Go Back!" };
            Back.Click += delegate { game.Menu(); };
            Size = new Size(1920, 1080);
            Controls.Add(Back);
        }

        public void Configure(GameEntity game)
        {
            if (this.game != null)
                return;

            this.game = game;
        }
    }
}
