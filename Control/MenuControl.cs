using Game.Model;
using Game.Model.Enums;
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
    public partial class MenuControl : UserControl
    {
        private GameEntity game;
        private Button Start;
        private Button Settings;
        private Button Exit;
        private TableLayoutPanel ButtonTable;

        public MenuControl()
        {
            InitializeComponent();
            Size = new Size(1920, 1080);
            InitializeTable();
        }

        public void Configure(GameEntity game)
        {
            if (this.game != null)
                return;

            this.game = game;
        }

        private void InitializeTable()
        {
            Start = new Button()
            {
                Image = new Bitmap(Folders.Buttons
                                          .Where(x => x == "C:\\Users\\TastePate\\OneDrive - УрФУ\\Рабочий стол\\Game\\Game\\bin\\Debug\\..\\..\\View\\Buttons\\PlayButton.png")
                                          .FirstOrDefault()),
                Size = new Size(200, 100)
            };
            Settings = new Button()
            {
                Image = new Bitmap(Folders.Buttons
                                          .Where(x => x == "C:\\Users\\TastePate\\OneDrive - УрФУ\\Рабочий стол\\Game\\Game\\bin\\Debug\\..\\..\\View\\Buttons\\OptionsButton.png")
                                          .FirstOrDefault()),
                Size = new Size(300, 100)
            };
            Exit = new Button()
            {
                Image = new Bitmap(Folders.Buttons
                                          .Where(x => x == "C:\\Users\\TastePate\\OneDrive - УрФУ\\Рабочий стол\\Game\\Game\\bin\\Debug\\..\\..\\View\\Buttons\\QuitButton.png")
                                          .FirstOrDefault()),
                Size = new Size(200, 100),
            };
            Start.FlatStyle = FlatStyle.Flat;
            Start.Dock = DockStyle.Fill;
            Start.FlatAppearance.BorderSize = 0;
            Start.FlatAppearance.MouseDownBackColor = Color.Transparent;
            Start.FlatAppearance.MouseOverBackColor = Color.Transparent;
            Settings.FlatStyle = FlatStyle.Flat;
            Settings.FlatAppearance.BorderSize = 0;
            Settings.FlatAppearance.MouseDownBackColor = Color.Transparent;
            Settings.FlatAppearance.MouseOverBackColor = Color.Transparent;
            Settings.Dock = DockStyle.Fill;
            Exit.FlatStyle = FlatStyle.Flat;
            Exit.FlatAppearance.BorderSize = 0;
            Exit.FlatAppearance.MouseDownBackColor = Color.Transparent;
            Exit.FlatAppearance.MouseOverBackColor = Color.Transparent;
            Exit.Dock = DockStyle.Fill;
            ButtonTable = new TableLayoutPanel
            {
                Location = new Point(600, 300),
                Size = new Size(300, 300),
            };
            ButtonTable.Controls.Add(Start, 0, 0);
            ButtonTable.Controls.Add(Settings, 0, 1);
            ButtonTable.Controls.Add(Exit, 0, 2);
            ButtonTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            ButtonTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            ButtonTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            ButtonTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300F));
            Start.MouseClick += (s, e) => { game.Configure(); game.Start(); };
            Settings.MouseClick += (s, e) => { game.Settings(); };
            Exit.MouseClick += (s, e) => { Application.Exit(); };
            Controls.Add(ButtonTable);
        }
    }
}
