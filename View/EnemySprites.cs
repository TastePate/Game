using Game.Model.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.View
{
    public static class EnemySprites
    {
        public static readonly Dictionary<Direction, Dictionary<EntityState, string[]>> Animations
            = new Dictionary<Direction, Dictionary<EntityState, string[]>>
            {
                [Direction.Left] = new Dictionary<EntityState, string[]>
                {
                    [EntityState.Idle] = Directory.GetFiles(Folders.EnemySpritesLeft[0]),
                    [EntityState.Move] = Directory.GetFiles(Folders.EnemySpritesLeft[1]),
                    [EntityState.TakeHit] = Directory.GetFiles(Folders.EnemySpritesLeft[2]),
                },
                [Direction.Right] = new Dictionary<EntityState, string[]>
                {
                    [EntityState.Idle] = Directory.GetFiles(Folders.EnemySpritesRight[0]),
                    [EntityState.Move] = Directory.GetFiles(Folders.EnemySpritesRight[1]),
                    [EntityState.TakeHit] = Directory.GetFiles(Folders.EnemySpritesRight[2]),
                }
            };
    }
}
