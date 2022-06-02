using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Extensions;

namespace Game.Model
{
    public static class Collisions
    {
        public static (bool OnGround, Platform Platform) HasPlayerCollisionWithPlatforms(Player player)
        {
            foreach (var platform in player.CurrentLevel.Platforms.Where(p => p.Position.Y < player.Position.Y + 100 
                                                                           && p.Position.Y > player.Position.Y - 100))
            {
                if (DoesIntersects(player.Hitbox, platform.Hitbox))
                {
                    player.CurrentPlatform = platform;
                    return (true, platform);
                }
            }
            return (false, player.CurrentPlatform);
        }

        public static (bool OnGround, Platform Platform) HasMonsterCollisionWithPlatforms(Monster monster)
        {
            foreach (var platform in monster.CurrentLevel.Platforms.Where(p => p.Position.Y < monster.Position.Y + 300
                                                                            && p.Position.Y > monster.Position.Y - 300))
            {
                if (DoesIntersects(monster.Hitbox, platform.Hitbox))
                {
                    monster.CurrentPlatform = platform;
                    return (true, platform);
                }
            }
            return (false, monster.CurrentPlatform);
        }

        public static (bool IsCollised, Monster Monster) HasCollisionWithMonsters(RectangleF hitbox, Level level)
        {
            foreach (var monster in level.Monsters.Where(m => m.Position.X > level.Player.Position.X - 100
                                                           && m.Position.X < level.Player.Position.X + 100
                                                           && m.Position.Y > level.Player.Position.Y - 100
                                                           && m.Position.Y < level.Player.Position.Y + 100))
            {
                if (DoesIntersects(hitbox, monster.Hitbox))
                    return (true, monster);
            }
            return (false, new Monster("no-collision", new PointF(), level));
        }

        public static (bool IsCollised, Monster Monster) WasMonsterAttacked(Line attackLine, Level level)
        {
            foreach (var monster in level.Monsters)
            {
                foreach (var side in monster.Hitbox.GetRectangleSides())
                {
                    var intersects = DoesLineCross(attackLine.Start, attackLine.End, side.Start, side.End);
                    if (intersects)
                        return (true, monster);
                }
            }
            return (false, new Monster("no-collision", new PointF(), level));
        }

        public static (bool IsCollised, Player player) HasCollisionWithPlayer(RectangleF hitbox, Level level)
        {
            if (DoesIntersects(hitbox, level.Player.Hitbox))
                return (true, level.Player);
            return (false, new Player(new PointF(), level));
        }

        public static bool HasCollision(RectangleF hitbox1, RectangleF hitbox2)
        {
            return hitbox1.IntersectsWith(hitbox2);
        }

        private static bool DoesIntersects(RectangleF hitbox, RectangleF platform)
        {
            return platform.Top - hitbox.Top < hitbox.Height + 1
                && platform.Top - hitbox.Top > hitbox.Height / 2
                && platform.Top >= hitbox.Top
                && hitbox.Left > platform.Left - hitbox.Width
                && hitbox.Left - platform.Left < platform.Size.Width;
        }

        private static float VectorMultiply(float ax, float ay, float bx, float by)
        {
            return ax * by - bx * ay;
        }

        private static bool DoesLineCross(PointF p1, PointF p2, PointF p3, PointF p4)
        {
            var v1 = VectorMultiply(p4.X - p3.X, p4.Y - p3.Y, p1.X - p3.X, p1.Y - p3.Y);
            var v2 = VectorMultiply(p4.X - p3.X, p4.Y - p3.Y, p2.X - p3.X, p2.Y - p3.Y);
            var v3 = VectorMultiply(p2.X - p1.X, p2.Y - p1.Y, p3.X - p1.X, p3.Y - p1.Y);
            var v4 = VectorMultiply(p2.X - p1.X, p2.Y - p1.Y, p4.X - p1.X, p4.Y - p1.Y);
            if ((v1 * v2) < 0 && (v3 * v4) < 0)
                return true;
            return false;
        }
    }
}
