using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.View
{
    public static class Folders
    {
        public static readonly string[] PlayerSpritesLeft = Directory.GetDirectories(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                                                     "..",
                                                                                     "..",
                                                                                     "View",
                                                                                     "PlayerSprites",
                                                                                     "Left"));
        public static readonly string[] PlayerSpritesRight = Directory.GetDirectories(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                                                      "..",
                                                                                      "..",
                                                                                      "View",
                                                                                      "PlayerSprites",
                                                                                      "Right"));
        public static readonly string[] EnemySpritesLeft = Directory.GetDirectories(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                                                    "..",
                                                                                    "..",
                                                                                    "View",
                                                                                    "EnemySprites",
                                                                                    "Left"));
        public static readonly string[] EnemySpritesRight = Directory.GetDirectories(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                                                     "..",
                                                                                     "..",
                                                                                     "View",
                                                                                     "EnemySprites",
                                                                                     "Right"));
        public static readonly string[] PlatfromSprites = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                                                   "..",
                                                                                   "..",
                                                                                   "View",
                                                                                   "PlatformSprites"));
        public static readonly string[] Background = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                                                   "..",
                                                                                   "..",
                                                                                   "View",
                                                                                   "Background"));
        public static readonly string[] Buttons = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                                                   "..",
                                                                                   "..",
                                                                                   "View",
                                                                                   "Buttons"));
        public static readonly string[] Logo = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                                                   "..",
                                                                                   "..",
                                                                                   "View",
                                                                                   "Logo"));
    }
}
