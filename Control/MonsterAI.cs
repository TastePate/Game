using Game.Model;
using Game.Model.Enums;
using Game.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Control
{
    public class MonsterAI
    {
        private readonly Monster monster;

        public MonsterAI(Monster monster)
        {
            this.monster = monster;
        }

        public void UpdateAI()
        {
            if (CheckIfPlayerOnSamePlatform())
            {
                monster.State = EntityState.Move;
                monster.Direction = monster.Position.X - monster.CurrentLevel.Player.Position.X < 0 ? Direction.Right : Direction.Left;
                monster.Run();
            }
            else
            {
                monster.State = EntityState.Idle;
                monster.StopRun();
            }
        }

        public bool CheckIfPlayerOnSamePlatform()
        {
            return monster.CurrentPlatform != null
                && monster.CurrentPlatform.Tag == monster.CurrentLevel.Player.CurrentPlatform?.Tag
                && monster.CurrentPlatform.Tag != "no-collision";
        }
    }
}
