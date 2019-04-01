using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocks
{
    class LevelEditorScreen : Screen
    {
        private Level level;
            
        public LevelEditorScreen(GraphicsDevice graphicsDevice, Game1 game1) : base(graphicsDevice, game1)
        {
            List<List<List<GameObject>>> levelGrid = new List<List<List<GameObject>>>();
            level = new Level(levelGrid, 0);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //draw level
            foreach (List<List<GameObject>> column in level.LevelObjects)
            {
                foreach(List<GameObject> tile in column)
                {
                    foreach(GameObject gameObject in tile)
                    {
                        gameObject.Draw(gameTime);
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
