using Game.Model;
using Game.Model.Enums;
using Game.View;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game.Extensions;

namespace Game.Control
{
    public partial class GameControl : UserControl
    {
        private GameEntity game;
        private Player player;
        private Level level;

        public GameControl()
        {
            Size = new Size(10000, 10000);
            Application.Idle += delegate { Invalidate(); };
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            InitializeComponent();
            BackgroundImage = new Bitmap(Folders.Background[0]);
        }

        public void Configure(GameEntity game)
        {
            this.game = game;
            player = game.Player;
            level = game.Levels[game.CurrentLevel];
            player.Animation.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Update();
            e.Graphics.DrawPlayerAttackLine(player);
            e.Graphics.DrawMonsters(level.Monsters);
            e.Graphics.DrawPlatforms(level);
            e.Graphics.DrawPlayer(player);
            e.Graphics.DrawPlayerHealth(player);
            e.Graphics.DrawMonstersHealth(level, player);
            e.Graphics.DrawLevelParametres(game, player, level);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F:
                    if (!player.IsAttacking)
                        PlayerAnimation.Frame = 0;
                    break;
                case Keys.Z:
                    if (!player.IsRolling)
                        PlayerAnimation.Frame = 0;
                    break;
            }
            PlayerControl.KeyDown(level.Player, e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            player.State = EntityState.Idle;
            PlayerControl.KeyUp(level.Player, e);
        }

        new private void Update()
        {
            game.Update();
            level = game.Levels[game.CurrentLevel];
            player = level.Player;
        }
    }
}
