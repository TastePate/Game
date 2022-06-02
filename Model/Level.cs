using Game.Extensions;
using Game.Model.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Game.Model
{
    public class Level
    {
        private readonly Random random = new Random();

        public readonly int LevelWidht;
        public readonly int LevelHeight;
        public readonly int PlatformsCount;
        public readonly int MonsterPosition;
        public readonly Difficulty Difficulty;

        public readonly List<Platform> Platforms;
        public readonly List<Monster> Monsters;
        public readonly List<Bitmap> PlatformsSprites;

        public Player Player { get; private set; }
        public bool LevelGenerated { get; private set; }

        public Level(int width, int weight, Difficulty difficulty)
        {
            if (!LevelGenerated)
            {
                Difficulty = difficulty;
                LevelWidht = width;
                LevelHeight = weight;
                PlatformsCount = random.Next(10, 30);
                Platforms = new List<Platform>();
                Monsters = new List<Monster>();
                GenerateLevel();
            }
        }

        public void GenerateLevel()
        {
            GeneratePlatforms();
            GenerateMonsters();
            GraphicsExtensions.CreatePlatformsLikeBitmaps(this);
            Player = new Player(GeneratePlayerPosition(), this);
            foreach (var monster in Monsters)
                monster.Animation.Start();
            LevelGenerated = true;
        }

        private PointF GeneratePlatformPosition()
        {
            var posX = random.Next(LevelWidht);
            var posY = random.Next(LevelHeight);
            var sign = random.Next(1, 3);
            if (Platforms.Count() != 0)
            {
                posX = sign == 1 ? random.Next((int)Platforms.Last().Position.X + 100, (int)Platforms.Last().Position.X + 200)
                                 : random.Next((int)Platforms.Last().Position.X - 200, (int)Platforms.Last().Position.X - 100);
                posY = random.Next((int)Platforms.Last().Position.Y + 100, (int)Platforms.Last().Position.Y + 120);
            }
            return new PointF(posX, posY);
        }

        private PointF GeneratePlayerPosition()
        {
            var randomPatform = Platforms.Last();
            return new PointF(randomPatform.Position.X + randomPatform.Width / 2,
                              randomPatform.Position.Y - 60);
        }

        private PointF GenerateMonsterPosition(Platform platform)
        {
            var posX = platform.Position.X + platform.Width / 2;
            var posY = platform.Position.Y - 20;
            return new PointF(posX, posY);
        }

        private void GeneratePlatforms()
        {
            for (var i = 0; i < PlatformsCount; i++)
            {
                var platformPosition = GeneratePlatformPosition();
                var platformWidth = random.Next(100, 1000);
                platformWidth = platformWidth + (15 - platformWidth % 15);
                var platform = new Platform("platform" + i.ToString(), platformPosition, platformWidth);
                Platforms.Add(platform);
            }
        }

        private void GenerateMonsters()
        {
            for (var i = 0; i < Platforms.Count - 1; i++)
            {
                var willGenerate = random.Next(1, 5);
                if (willGenerate == 1 && Platforms[i].Width > 200 || Platforms[i].Width > 800)
                    Monsters.Add(new Monster("monster" + i.ToString(), 
                                             GenerateMonsterPosition(Platforms[i]), 
                                             this)
                    { 
                        Speed = 10 + (int)Difficulty * 5 
                    });
            }
        }
    }
}
