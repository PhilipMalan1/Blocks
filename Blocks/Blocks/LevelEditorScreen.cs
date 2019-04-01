using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Blocks
{
    class LevelEditorScreen : Screen
    {
        private Level level;
        float blockWidth = 108;
            
        public LevelEditorScreen(GraphicsDevice graphicsDevice, Game1 game1) : base(graphicsDevice, game1)
        {
            List<List<List<GameObject>>> levelGrid = new List<List<List<GameObject>>>();

            level = new Level(levelGrid, 0);

            //TODO test
            level.AddGameObject(new Ground(LoadedContent.ground, new Rectangle(0, 0, 108, 108)), 0, 0);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            //draw level
            foreach (List<List<GameObject>> column in level.LevelObjects)
            {
                foreach(List<GameObject> tile in column)
                {
                    foreach(GameObject gameObject in tile)
                    {
                        gameObject.Draw(gameTime, spriteBatch, new Vector2());
                    }
                }
            }

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
