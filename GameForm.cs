using Game.Control;
using Game.Model;
using Game.Model.Enums;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Game
{
    public partial class GameForm : Form
    {
        private MenuControl MenuControl = new MenuControl();
        private SettingsControl SettingsControl = new SettingsControl();
        private GameControl GameControl = new GameControl();
        private WinControl WinControl = new WinControl();
        private GameEntity Game;
        private DeadControl DeadControl = new DeadControl();

        public GameForm()
        {
            InitializeComponent();
            Size = new Size(1920, 1080);
            TopMost = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            Game = new GameEntity(3);
            Game.OnStageChanged += Game_OnStageChanged;
            Controls.Add(MenuControl);
            Controls.Add(SettingsControl);
            Controls.Add(GameControl);
            Controls.Add(WinControl);
            Controls.Add(DeadControl);
            ShowMenu();
        }

        private void Game_OnStageChanged(GameStage stage)
        {
            switch (stage)
            {
                case GameStage.Menu:
                    ShowMenu();
                    break;
                case GameStage.Game:
                    StartGame();
                    break;
                case GameStage.Settings:
                    ShowSettings();
                    break;
                case GameStage.DeadScreen:
                    ShowDeadScreen();
                    break;
                case GameStage.WinScreen:
                    ShowWinScreen();
                    break;
            }
        }

        private void ShowWinScreen()
        {
            HideScreens();
            WinControl.Configure(Game);
            WinControl.Show();
            WinControl.Focus();
        }

        private void ShowDeadScreen()
        {
            HideScreens();
            DeadControl.Configure(Game);
            DeadControl.Show();
            DeadControl.Focus();
        }

        private void ShowSettings()
        {
            HideScreens();
            SettingsControl.Configure(Game);
            SettingsControl.Show();
            SettingsControl.Focus();
        }

        private void StartGame()
        {
            HideScreens();
            Game.Configure();
            GameControl.Configure(Game);
            GameControl.Show();
            GameControl.Focus();
        }

        private void ShowMenu()
        {
            HideScreens();
            MenuControl.Configure(Game);
            MenuControl.Show();
            MenuControl.Focus();
        }

        private void HideScreens()
        {
            WinControl.Hide();
            MenuControl.Hide();
            SettingsControl.Hide();
            GameControl.Hide();
        }
    }
}
