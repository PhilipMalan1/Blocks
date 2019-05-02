using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    class LevelManager
    {
        private static string[] levels = { "Level 0", "Level 1" };

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
