using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    class LevelManager
    {
        private static string[] levels = { @"Content/Levels/Level 0.dat", @"Content/Levels/Level 1.dat", @"Content/Levels/philipLevel 2.dat",
            @"Content/Levels/philipLevel 3.dat", @"Content/Levels/philipLevel 4.dat", @"Content/Levels/philipLevel 5.dat", @"Content/Levels/philipLevel 6.dat",
            @"Content/Levels/philipLevel 7.dat"};

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
