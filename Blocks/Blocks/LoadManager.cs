using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    public class LoadManager
    {
        private Level level;
        private Rectangle screeni;
        private float blockWidth;

        public LoadManager(Level level, float blockWidth)
        {
            this.level = level;
            this.blockWidth = blockWidth;
        }

        public List<GameObject> LoadFirstScreen(Rectangle screen)
        {
            List<GameObject> loaded = new List<GameObject>();

            int startX = (int)(screen.X / blockWidth) - 5;
            int endX = (int)((screen.X + screen.Width) / blockWidth + 1) + 5;
            int endY = (int)(-screen.Y / blockWidth) + 5;
            int startY = (int)((-screen.Y - screen.Height) / blockWidth - 1) - 5;

            for(int x=startX; x<=endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    if(level.CheckForObject(x, y))
                    {
                        for (int i = 0; i < level.LevelObjects[x][y].Count(); i++)
                        {
                            if (level.LevelObjects[x][y][i].LoadRect.Intersects(screen))
                            {
                                loaded.Add(level.LevelObjects[x][y][i]);
                                level.LevelObjects[x][y][i].Load();
                            }
                        }
                    }
                }
            }

            screeni = screen;
            return loaded;
        }

        public void Update(Rectangle screen, List<GameObject> loaded)
        {
            int startX = (int)(Math.Min(screen.X, screeni.X) / blockWidth)-5;
            int endX = (int)(Math.Max(screen.X + screen.Width, screeni.X + screeni.Width) / blockWidth + 1)+5;
            int endY= (int)(-Math.Max(screen.Y, screeni.Y) / blockWidth)+5;
            int startY = (int)(-Math.Min(screen.Y + screen.Height, screeni.Y + screeni.Height) / blockWidth - 1)-5;
         
            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    if (level.CheckForObject(x, y))
                    {
                        for (int i = 0; i < level.LevelObjects[x][y].Count(); i++)
                        {
                            if (level.LevelObjects[x][y][i].LoadRect.Intersects(screen))
                            {
                                if(!loaded.Contains(level.LevelObjects[x][y][i]))
                                {
                                    loaded.Add(level.LevelObjects[x][y][i]);
                                    level.LevelObjects[x][y][i].Load();
                                }
                            }
                        }
                    }
                }
            }

            for(int i=0; i<loaded.Count(); i++)
            {
                if (!loaded[i].LoadRect.Intersects(screen))
                {
                    loaded[i].Unload();
                    loaded.RemoveAt(i);
                    i--;
                }
            }

            screeni = screen;
        }
    }
}
