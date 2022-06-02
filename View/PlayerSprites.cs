using Game.Model.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.View
{
    public static class PlayerSprites
    {
        public static readonly Dictionary<Direction, Dictionary<EntityState, string[]>> Animations
            = new Dictionary<Direction, Dictionary<EntityState, string[]>>
            {
                [Direction.Left] = new Dictionary<EntityState, string[]>
                {
                    [EntityState.Idle] = Directory.GetFiles(Folders.PlayerSpritesLeft[0]),
                    [EntityState.Move] = Directory.GetFiles(Folders.PlayerSpritesLeft[1]),
                    [EntityState.Down] = Directory.GetFiles(Folders.PlayerSpritesLeft[2]),
                    [EntityState.Up] = Directory.GetFiles(Folders.PlayerSpritesLeft[4]),
                    [EntityState.Roll] = Directory.GetFiles(Folders.PlayerSpritesLeft[5]),
                    [EntityState.Attack] = Directory.GetFiles(Folders.PlayerSpritesLeft[6]),
                    [EntityState.Die] = Directory.GetFiles(Folders.PlayerSpritesLeft[12]),
                },
                [Direction.Right] = new Dictionary<EntityState, string[]>
                {
                    [EntityState.Idle] = Directory.GetFiles(Folders.PlayerSpritesRight[0]),
                    [EntityState.Move] = Directory.GetFiles(Folders.PlayerSpritesRight[1]),
                    [EntityState.Down] = Directory.GetFiles(Folders.PlayerSpritesRight[2]),
                    [EntityState.Up] = Directory.GetFiles(Folders.PlayerSpritesRight[4]),
                    [EntityState.Roll] = Directory.GetFiles(Folders.PlayerSpritesRight[5]),
                    [EntityState.Attack] = Directory.GetFiles(Folders.PlayerSpritesRight[6]),
                    [EntityState.Die] = Directory.GetFiles(Folders.PlayerSpritesRight[12]),
                }
            };
    }
}
