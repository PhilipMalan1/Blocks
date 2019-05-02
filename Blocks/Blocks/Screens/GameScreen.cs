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
        private string levelDir;
        private Level level;
        KeyboardState key, keyi;
        MouseState mouse, mousei;
        float blockWidth;
        Action OnCompletion;

        public string LevelDir
        {
            get
            {
                return levelDir;
            }

            set
            {
                levelDir = value;
            }
        }

        public Action OnCompletion1
        {
            get
            {
                return OnCompletion;
            }

            set
            {
                OnCompletion = value;
            }
        }

        public GameScreen(GraphicsDevice graphicsDevice, Game1 game1, string levelDir, Action OnCompletion) : base(graphicsDevice, game1)
        {
            this.levelDir = levelDir;
            this.OnCompletion = OnCompletion;

            key = Keyboard.GetState();
            mouse = Mouse.GetState();
            blockWidth = graphicsDevice.Viewport.Height / 15;

            Load();
        }

        public GameScreen(GraphicsDevice graphicsDevice, Game1 game1, Level level, Action OnCompletion) : base(graphicsDevice, game1)
        {
            this.level = level;
            this.OnCompletion = OnCompletion;

            key = Keyboard.GetState();
            mouse = Mouse.GetState();
            blockWidth = graphicsDevice.Viewport.Height / 15;

            level.Initialize(blockWidth, graphicsDevice, ()=>
            {
                game1.SetScreen(this);
            });
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, blendState: BlendState.AlphaBlend);

            //draw level
            foreach (GameObject gameObject in level.Loaded)
            {
                gameObject.Draw(gameTime, spriteBatch, level.Camera);
            }

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            level.Update(gameTime);

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
        }

        public void Load()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(levelDir, FileMode.Open, FileAccess.Read, FileShare.Read);
            level = (Level)formatter.Deserialize(stream);
            stream.Close();
            level.Initialize(blockWidth, graphicsDevice, ()=>
            {
                OnCompletion.Invoke();
            });
        }
    }
}
