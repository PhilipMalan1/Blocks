using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Xna.Framework.Input;

namespace Blocks
{
    class GameScreen : Screen
    {
        string levelDir;
        private Level level;
        KeyboardState key, keyi;
        MouseState mouse, mousei;
        Vector2 camera;
        float blockWidth;

        public GameScreen(GraphicsDevice graphicsDevice, Game1 game1, string levelDir) : base(graphicsDevice, game1)
        {
            this.levelDir = levelDir;

            key = Keyboard.GetState();
            mouse = Mouse.GetState();
            blockWidth = graphicsDevice.Viewport.Height / 15;

            Load();
            camera = level.CameraFocus.Pos + new Vector2(blockWidth / 2, blockWidth / 2) - new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);
        }

        public GameScreen(GraphicsDevice graphicsDevice, Game1 game1, Level level) : base(graphicsDevice, game1)
        {
            this.level = level;

            key = Keyboard.GetState();
            mouse = Mouse.GetState();
            blockWidth = graphicsDevice.Viewport.Height / 15;

            level.Initialize(blockWidth);
            camera = level.CameraFocus.Pos + new Vector2(blockWidth / 2, blockWidth / 2) - new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, blendState: BlendState.AlphaBlend);

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
            //input updates
            keyi = key;
            key = Keyboard.GetState();
            mousei = mouse;
            mouse = Mouse.GetState();
            foreach(IInput inputObject in level.InputObjects)
            {
                inputObject.UpdateInput(gameTime, key, keyi, mouse, mousei);
            }

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

            //update physics
            level.PhysicsMangager.Update(gameTime);

            //move camera
            Vector2 destination = level.CameraFocus.Pos+new Vector2(blockWidth/2, blockWidth/2)-new Vector2(graphicsDevice.Viewport.Width/2, graphicsDevice.Viewport.Height/2);
            if (destination.X < 0)
                destination.X = 0;
            if (destination.Y > -graphicsDevice.Viewport.Height)
                destination.Y = -graphicsDevice.Viewport.Height;
            camera += (destination - camera) / 5;
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
