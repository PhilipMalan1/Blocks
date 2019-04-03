using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Blocks
{
    class LevelEditorScreen : Screen
    {
        private Level level;
        Vector2 camera;
        float blockWidth;
        KeyboardState key, keyi;
        MouseState mouse, mousei;
            
        public LevelEditorScreen(GraphicsDevice graphicsDevice, Game1 game1) : base(graphicsDevice, game1)
        {
            key = Keyboard.GetState();
            mouse = Mouse.GetState();

            blockWidth = graphicsDevice.Viewport.Height / 10;

            List<List<List<GameObject>>> levelGrid = new List<List<List<GameObject>>>();

            level = new Level(levelGrid, 0);

            camera = new Vector2(0, -graphicsDevice.Viewport.Height);

            //TODO test
            //level.AddGameObject(new Ground(level, blockWidth, new Vector2(0, -blockWidth)), 0, 0);
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
            keyi = key;
            key = Keyboard.GetState();
            mousei = mouse;
            mouse = Mouse.GetState();

            //draw blocks
            if(mouse.LeftButton==ButtonState.Pressed)
            {
                int x = (int)((mouse.X - camera.X) / blockWidth);
                if (x < 0) x = 0;
                int y = (int)((-camera.Y-mouse.Y) / blockWidth)+1;
                if (y < 0) y = 0;
                if (!level.CheckForObject(x, y))
                    AddTile(GameObjects.Ground, x, y);
            }

            //delet blocks
            if(mouse.RightButton==ButtonState.Pressed)
            {
                int x = (int)((mouse.X - camera.X) / blockWidth);
                if (x < 0) x = 0;
                int y = (int)((-camera.Y - mouse.Y) / blockWidth) + 1;
                if (y < 0) y = 0;
                level.RemoveGameObject(x, y);
            }

            //move camera

        }

        public void AddTile(GameObjects gameObject, int x, int y)
        {
            switch(gameObject)
            {
                case GameObjects.Ground:
                    level.AddGameObject(new Ground(level, blockWidth, new Vector2(blockWidth * x, -blockWidth * y)), x, y);
                    break;
            }
        }
    }

    enum GameObjects
    {
        Ground
    }
}
