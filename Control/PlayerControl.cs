using Game.Model;
using Game.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Control
{
    public static class PlayerControl
    {
        public static void KeyDown(Player player, KeyEventArgs e)
        {
            if (!player.IsRolling && !player.IsAttacking)
            {
                if (e.KeyCode == Keys.D)
                {
                    player.Direction = Direction.Right;
                    player.Run();
                }
                if (e.KeyCode == Keys.A)
                {
                    player.Direction = Direction.Left;
                    player.Run();
                }
                if (e.KeyCode == Keys.Space && player.OnGround)
                {
                    player.Jump();
                }
                if (e.KeyCode == Keys.Z && player.OnGround && !player.IsRolling)
                {
                    player.IsRolling = true;
                    player.Roll();
                }
                if (e.KeyCode == Keys.S)
                {
                    player.IgnoreGroundCollision = true;
                }
                if (e.KeyCode == Keys.F && player.OnGround)
                {
                    player.Attack();
                    player.IsAttacking = true;
                }
            }
        }

        public static void KeyUp(Player player, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.A)
            {
                player.StopRun();
            }
        }
    }
}
