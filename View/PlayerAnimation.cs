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
    public class PlayerAnimation
    {
        public static int Frame { get; set; }
        private Player player;
        private Timer animationTimer;

        public PlayerAnimation(Player player)
        {
            this.player = player;
            animationTimer = new Timer() { Interval = 70 };
            animationTimer.Tick += (s, e) => UpdateAnimation();
        }

        public void Start() => animationTimer.Start();

        public void DrawFrame(Graphics g)
        {
            var img = ChooseAnimation();
            g.DrawImage(img,
                        new PointF(player.Position.X - img.Width / 2 + player.Width / 2 + player.Camera.X,
                                   player.Position.Y - img.Height / 2 - player.Height / 2 + 5 + player.Camera.Y));
        }

        private void UpdateAnimation() => Frame++;

        private Bitmap ChooseAnimation()
        {
            var img = new Bitmap(player.Animations[player.Direction][player.State][0]);
            if (player.IsFalling && !player.OnGround)
            {
                img = SetAnimation(EntityState.Down);
                player.IsRolling = false;
            }
            if (player.IsJumping && !player.OnGround)
            {
                img = SetAnimation(EntityState.Up);
            }
            if (player.OnGround && !player.IsAttacking)
            {
                img = SetAnimation(EntityState.Idle);
            }
            if (player.IsMoving && player.OnGround && !player.IsAttacking)
            {
                img = SetAnimation(EntityState.Move);
            }
            if (player.IsRolling && player.OnGround && !player.IsAttacking)
            {
                img = SetAnimation(EntityState.Roll);
                if (Frame == PlayerSprites.Animations[player.Direction][EntityState.Roll].Length - 1)
                    player.IsRolling = false;
            }
            if (player.IsAttacking && player.OnGround && !player.IsRolling)
            {
                player.Attack();
                img = SetAnimation(EntityState.Attack);
                if (Frame == PlayerSprites.Animations[player.Direction][EntityState.Attack].Length - 2)
                {
                    player.AttackLine = new Line();
                    player.IsAttacking = false;
                }
            }
            if (player.IsDead)
            {
                player.IsAttacking = false;
                player.IsRolling = false;
                img = SetAnimation(EntityState.Die);
            }
            return img;
        }

        private Bitmap SetAnimation(EntityState state)
        {
            player.State = state;
            StartAnimationAgainIfNotInBounds();
            return new Bitmap(PlayerSprites.Animations[player.Direction][state][Frame]);
        }

        private void StartAnimationAgainIfNotInBounds()
        {
            if (Frame > PlayerSprites.Animations[player.Direction][player.State].Length - 1)
                Frame = 0;
        }
    }
}
