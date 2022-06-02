using Game.Model;
using Game.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Extensions
{
    public static class GraphicsExtensions
    {
        public static void DrawMonsters(this Graphics g, List<Monster> monsters)
        {
            foreach (var monster in monsters)
                monster.Animation.DrawFrame(g);
        }

        public static void DrawPlayer(this Graphics g, Player player)
        {
            player.Animation.DrawFrame(g);
        }

        public static void DrawPlatformsLikeRectangles(this Graphics g, Level level)
        {
            foreach (var platform in level.Platforms)
                g.FillRectangle(Brushes.Green, new RectangleF(new PointF(platform.Hitbox.X + level.Player.Camera.X, platform.Hitbox.Y + level.Player.Camera.Y),
                                                                         platform.Size));
        }

        public static void DrawPlayerAttackLine(this Graphics g, Player player)
        {
            g.DrawLine(Pens.Black,
                       player.AttackLine.Start.X + player.Camera.X,
                       player.AttackLine.Start.Y + player.Camera.Y,
                       player.AttackLine.End.X + player.Camera.X,
                       player.AttackLine.End.Y + player.Camera.Y);
        }

        public static void DrawPlatforms(this Graphics g, Level level)
        {
            foreach (var platform in level.Platforms)
                g.DrawImage(platform.Sprite, new PointF(platform.Hitbox.X + level.Player.Camera.X, platform.Hitbox.Y + level.Player.Camera.Y));
        }

        public static void CreatePlatformsLikeBitmaps(Level level)
        {
            foreach (var platform in level.Platforms)
                platform.Sprite = JoinPlatformPieces((int)(platform.Hitbox.Width / 15));
        }

        public static void DrawLevelParametres(this Graphics g, GameEntity game, Player player, Level level)
        {
            g.DrawString($"Level: {game.CurrentLevel}\nDifficulty: {level.Difficulty}\nSlimes Left: {level.Monsters.Count}",
                                  new Font(FontFamily.GenericMonospace, 10, FontStyle.Bold),
                                  Brushes.Black,
                                  new Point((int)(player.Position.X + player.Camera.X - 25),
                                            (int)(player.Position.Y + player.Camera.Y - 80)));
        }

        public static void DrawMonstersHealth(this Graphics g, Level level, Player player)
        {
            foreach (var monster in level.Monsters)
            {
                g.DrawString($"{monster.HealthPoint} / 60",
                                      new Font(FontFamily.GenericMonospace, 10, FontStyle.Bold),
                                      Brushes.Red,
                                      new Point((int)(monster.Position.X + player.Camera.X - 25),
                                                (int)(monster.Position.Y + player.Camera.Y - 30)));
            }
        }

        public static void DrawPlayerHealth(this Graphics g, Player player)
        {
            g.DrawString($"{player.HealthPoint} / 100",
                                  new Font(FontFamily.GenericMonospace, 10, FontStyle.Bold),
                                  Brushes.Red,
                                  new Point((int)(player.Position.X + player.Camera.X - 25),
                                            (int)(player.Position.Y + player.Camera.Y - 30)));

        }

        private static Bitmap JoinPlatformPieces(int pieces)
        {
            var left = new Bitmap(Folders.PlatfromSprites[0]);
            var middle = new Bitmap(Folders.PlatfromSprites[1]);
            var right = new Bitmap(Folders.PlatfromSprites[2]);
            var result = new Bitmap(pieces * 15, 30);
            var g = Graphics.FromImage(result);
            g.DrawImage(left, new PointF());
            for (var i = 1; i < pieces - 1; i++)
                g.DrawImage(middle, new PointF(i * 15, 0));
            g.DrawImage(right, new PointF(pieces * 15 - 15, 0));
            return result;
        }
    }
}
