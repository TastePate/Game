using Game.Model.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Game.Model
{
    public class GameEntity
    {
        private DateTime lastUpdate = DateTime.MinValue;
        public event Action<GameStage> OnStageChanged;
        public GameStage GameStage { get; set; } = GameStage.Menu;
        public int LevelsCount { get; }
        public int CurrentLevel { get; private set; }
        public Player Player { get { return Levels[CurrentLevel].Player; } }
        public List<Monster> Monsters { get { return Levels[CurrentLevel].Monsters; } }
        public List<Level> Levels { get; private set; }

        public GameEntity(int levelsCount)
        {
            LevelsCount = levelsCount;
            Levels = new List<Level>();
        }

        public void Configure()
        {
            Levels = new List<Level>();
            InitializeLevels();
        }

        public void Update()
        {
            var now = DateTime.Now;
            var dt = (float)(now - lastUpdate).TotalMilliseconds / 100f;

            if (lastUpdate != DateTime.MinValue && !Player.IsDead)
            {
                Player.Update(dt);
                foreach (var monster in Monsters)
                {
                    if (monster.IsDead)
                    {
                        Monsters.Remove(monster);
                        break;
                    }
                    monster.Update(dt);
                }
                if (Player.IsDead)
                    DeadScreen();
                if (Monsters.Count == 0)
                    ChangeLevel();
            }

            lastUpdate = now;
        }

        public void InitializeLevels()
        {
            for (var i = 0; i < LevelsCount; i++)
            {
                var level = new Level(800, 600, (Difficulty)i);
                Levels.Add(level);
            }
        }

        public void ChangeLevel()
        {
            if (CurrentLevel + 1 == LevelsCount)
            {
                CurrentLevel = 0;
                WinScreen();
            }
            else
                CurrentLevel++;
        }

        private void ChangeStage(GameStage stage)
        {
            GameStage = stage;
            OnStageChanged?.Invoke(stage);
        }

        public void Settings()
        {
            ChangeStage(GameStage.Settings);
        }

        public void Start()
        {
            ChangeStage(GameStage.Game);
        }

        public void Menu()
        {
            ChangeStage(GameStage.Menu);
        }

        public void DeadScreen()
        {
            ChangeStage(GameStage.DeadScreen);
        }

        public void WinScreen()
        {
            ChangeStage(GameStage.WinScreen);
        }
    }
}
