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
        float blockWidth;
        Texture2D background;
        int timer;
        int currentBack;
        bool flaggy;
        public GameScreen(GraphicsDevice graphicsDevice, Game1 game1, string levelDir) : base(graphicsDevice, game1)
        {
            this.levelDir = levelDir;

            key = Keyboard.GetState();
            mouse = Mouse.GetState();
            blockWidth = graphicsDevice.Viewport.Height / 15;
            background = LoadedContent.bg1;
            flaggy = true;
            Load();
        }

        public GameScreen(GraphicsDevice graphicsDevice, Game1 game1, Level level) : base(graphicsDevice, game1)
        {
            this.level = level;

            key = Keyboard.GetState();
            mouse = Mouse.GetState();
            blockWidth = graphicsDevice.Viewport.Height / 15;
            background = LoadedContent.bg1;
            flaggy = true;
            level.Initialize(blockWidth, graphicsDevice);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, blendState: BlendState.AlphaBlend);
            //draw background 
            spriteBatch.Draw(background, new Rectangle(0,0,graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height),new Rectangle(0,0, 1920, 1080), Color.White,0,new Vector2(0,0), SpriteEffects.None,(float)DrawLayer.Background/1000);
            

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
            //update the background

            timer++;
            if (timer % 10 == 0 && flaggy)
                currentBack++;
            if (timer % 10 == 0 && !flaggy)
                currentBack--;
            switch (currentBack)
            {
                case (-1):
                    {
                        flaggy = true;
                        currentBack +=2;
                        break;
                    }
                case (0):
                    {
                        background = LoadedContent.bg1;
                        break;
                    }
                case (1):
                    {
                        background = LoadedContent.bg2;
                        break;
                    }
                case (2):
                    {
                        background = LoadedContent.bg3;
                        break;
                    }
                case (3):
                    {
                        background = LoadedContent.bg4;
                        break;
                    }
                case (4):
                    {
                        background = LoadedContent.bg5;
                        break;
                    }
                case (5):
                    {
                        background = LoadedContent.bg6;
                        break;
                    }
                case (6):
                    {
                        background = LoadedContent.bg7;
                        break;
                    }
                case (7):
                    {
                        background = LoadedContent.bg8;
                        break;
                    }
                case (8):
                    {
                        flaggy = false;
                        currentBack -= 2;
                        break;
                    }
            }

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
            level.Initialize(blockWidth, graphicsDevice);
        }
    }
}
