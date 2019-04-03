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
        PhysicsManager physicsManager;
        Vector2 camera;
        float blockWidth;
            
        public LevelEditorScreen(GraphicsDevice graphicsDevice, Game1 game1) : base(graphicsDevice, game1)
        {
            blockWidth = graphicsDevice.Viewport.Height / 10;

            physicsManager = new PhysicsManager();

            List<List<List<GameObject>>> levelGrid = new List<List<List<GameObject>>>();

            level = new Level(levelGrid, 0);

            camera = new Vector2(0, -graphicsDevice.Viewport.Height);

            //TODO test
            level.AddGameObject(new Ground(physicsManager, blockWidth, new Vector2(0, -blockWidth)), 0, 0);
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
                        gameObject.Draw(gameTime, spriteBatch, camera);
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
