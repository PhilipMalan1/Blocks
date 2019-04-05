using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Blocks
{
    class GameScreen : Screen
    {
        string levelDir;
        private Level level;
        Vector2 camera;
        float blockWidth;

        public GameScreen(GraphicsDevice graphicsDevice, Game1 game1, string levelDir) : base(graphicsDevice, game1)
        {
            this.levelDir = levelDir;

            blockWidth = graphicsDevice.Viewport.Height / 10;
            camera = new Vector2(0, -graphicsDevice.Viewport.Height);

            Load();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            //draw level
            foreach (List<List<GameObject>> column in level.LevelObjects)
            {
                foreach (List<GameObject> tile in column)
                {
                    foreach (GameObject gameObject in tile)
                    {
                        gameObject.Draw(gameTime, spriteBatch, camera);
                    }
                }
            }

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            //update level
            foreach (List<List<GameObject>> column in level.LevelObjects)
            {
                foreach (List<GameObject> tile in column)
                {
                    foreach (GameObject gameObject in tile)
                    {
                        gameObject.Update(gameTime);
                    }
                }
            }

            level.PhysicsMangager.Update(gameTime);
        }

        public void Load()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(levelDir, FileMode.Open, FileAccess.Read, FileShare.Read);
            level = (Level)formatter.Deserialize(stream);
            stream.Close();
            level.Initialize(blockWidth);
        }
    }
}
