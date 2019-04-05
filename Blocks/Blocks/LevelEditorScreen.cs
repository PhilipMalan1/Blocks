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

            //draw tiles
            Array GameObjectList=Enum.GetValues(typeof(GameObjects));
            for(int x=0; x<graphicsDevice.Viewport.Width/(int)blockWidth; x++)
            {
                if (x < GameObjectList.GetLength(0))
                    DrawIcon((GameObjects)GameObjectList.GetValue(0), gameTime, spriteBatch, x * (int)blockWidth, 0);
            }

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            keyi = key;
            key = Keyboard.GetState();
            mousei = mouse;
            mouse = Mouse.GetState();

            int x = (int)((mouse.X + camera.X) / blockWidth);
            if (x < 0) x = 0;
            int y = (int)-((int)(camera.Y + mouse.Y) / blockWidth) + 1;
            if (y < 0) y = 0;

            //draw blocks
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                if (!level.CheckForObject(x, y))
                    AddTile(GameObjects.Ground, x, y);
            }

            //delet blocks
            if (mouse.RightButton == ButtonState.Pressed)
            {
                level.RemoveGameObject(x, y);
            }

            //move camera
            float cameraSpeed = blockWidth;
            if (key.IsKeyDown(Keys.Right)) camera.X += cameraSpeed;
            if (key.IsKeyDown(Keys.Left)) camera.X -= cameraSpeed;
            if (key.IsKeyDown(Keys.Up)) camera.Y -= cameraSpeed;
            if (key.IsKeyDown(Keys.Down)) camera.Y += cameraSpeed;
            if (camera.X < 0) camera.X = 0;
            if (camera.Y > -graphicsDevice.Viewport.Height) camera.Y = -graphicsDevice.Viewport.Height;
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

        public void DrawIcon(GameObjects gameObject, GameTime gameTime, SpriteBatch spriteBatch, int x, int y)
        {
            switch(gameObject)
            {
                case GameObjects.Ground:
                    Ground.DrawIcon(gameTime, spriteBatch, new Rectangle(x, y, (int)blockWidth, (int)blockWidth));
                    break;
            }
        }
    }

    enum GameObjects
    {
        Ground
    }
}
