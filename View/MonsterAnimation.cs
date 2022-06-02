using Game.Model;
using Game.Model.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.View
{
    public class MonsterAnimation
    {
        public int Frame { get; set; }
        private Monster monster;
        private Timer animationTimer;

        public MonsterAnimation(Monster monster)
        {
            this.monster = monster;
            animationTimer = new Timer() { Interval = 70 };
            animationTimer.Tick += (s, e) => UpdateAnimation();
        }

        public void Start() => animationTimer.Start();

        public void DrawFrame(Graphics g)
        {
            var img = ChooseAnimation();
            if (!monster.IsDead)
                g.DrawImage(img,
                            new PointF(monster.Position.X - img.Width / 2 + monster.Width / 2 + monster.CurrentLevel.Player.Camera.X,
                                       monster.Position.Y - img.Height / 2 - monster.Height * 3 + monster.CurrentLevel.Player.Camera.Y));
        }

        private Bitmap ChooseAnimation()
        {
            var img = new Bitmap(monster.Animations[monster.Direction][monster.State][0]);
            if (monster.OnGround)
            {
                img = SetAnimation(EntityState.Idle);
            }
            if (monster.IsMoving && monster.OnGround)
            {
                img = SetAnimation(EntityState.Move);
            }
            return img;
        }

        private void UpdateAnimation() => Frame++;

        private void StartAnimationAgainIfNotInBounds()
        {
            if (Frame > EnemySprites.Animations[monster.Direction][monster.State].Length - 1)
                Frame = 0;
        }

        private Bitmap SetAnimation(EntityState state)
        {
            monster.State = state;
            StartAnimationAgainIfNotInBounds();
            return new Bitmap(EnemySprites.Animations[monster.Direction][state][Frame]);
        }
    }
}
