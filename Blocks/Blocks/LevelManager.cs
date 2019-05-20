using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    class LevelManager
    {
        private static string[] levels = { @"Content/Levels/Level 3.dat", @"Content/Levels/Level 69.dat" };

        public static string firstLevel()
        {
            return levels[0];
        }

        public static string nextLevel(string current)
        {
            int newIndex = Array.IndexOf(levels, current) + 1;
            if (newIndex < levels.Count())
                return levels[newIndex];
            else
                return null;
        }
    }
}
