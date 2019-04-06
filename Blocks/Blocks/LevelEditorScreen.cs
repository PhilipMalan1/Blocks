﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Blocks
{
    class LevelEditorScreen : Screen
    {
        private Level level;
        Vector2 camera;
        float blockWidth;
        KeyboardState key, keyi;
        MouseState mouse, mousei;
        string levelDir;

        GameObjects current;
        int objectRow, objectsPerRow;
            
        public LevelEditorScreen(GraphicsDevice graphicsDevice, Game1 game1, string levelDir) : base(graphicsDevice, game1)
        {
            this.levelDir = levelDir;

            key = Keyboard.GetState();
            mouse = Mouse.GetState();

            blockWidth = graphicsDevice.Viewport.Height / 10;

            List<List<List<GameObject>>> levelGrid = new List<List<List<GameObject>>>();

            level = new Level(levelGrid, 0, blockWidth);

            camera = level.CameraFocus.Pos-new Vector2(graphicsDevice.Viewport.Width/2, graphicsDevice.Viewport.Height/2);

            current = 0;
            objectRow = 0;
            objectsPerRow = graphicsDevice.Viewport.Width / (int)blockWidth;

            Load();
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

            //draw GameObject selecter
            Array GameObjectList=Enum.GetValues(typeof(GameObjects));
            for(int x=0; x<graphicsDevice.Viewport.Width/(int)blockWidth; x++)
            {
                if (x < GameObjectList.GetLength(0))
                    DrawIcon((GameObjects)GameObjectList.GetValue(x), gameTime, spriteBatch, x * (int)blockWidth, 0);
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

            //left click
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                //pick block
                int blockX = mouse.X / (int)blockWidth;
                int selectedBlock = blockX + (objectRow * objectsPerRow);
                if (mouse.Y < blockWidth && blockX < objectsPerRow && selectedBlock < Enum.GetNames(typeof(GameObjects)).Count())
                {
                    if (mousei.LeftButton == ButtonState.Released)
                    {
                        current = (GameObjects)selectedBlock;
                    }
                }
                //draw blocks
                else if (!level.CheckForObject(x, y))
                    AddTile(current, x, y);
            }

            //delet blocks
            if (mouse.RightButton == ButtonState.Pressed)
            {
                level.RemoveGameObject(x, y);
            }

            //move camera
            float cameraSpeed = blockWidth;
            if (key.IsKeyDown(Keys.D)) camera.X += cameraSpeed;
            if (key.IsKeyDown(Keys.A)) camera.X -= cameraSpeed;
            if (key.IsKeyDown(Keys.W)) camera.Y -= cameraSpeed;
            if (key.IsKeyDown(Keys.S)) camera.Y += cameraSpeed;
            if (camera.X < 0) camera.X = 0;
            if (camera.Y > -graphicsDevice.Viewport.Height) camera.Y = -graphicsDevice.Viewport.Height;

            //save
            if (key.IsKeyDown(Keys.Escape) || (key.IsKeyDown(Keys.Q) && keyi.IsKeyUp(Keys.Q)))
                Save();
        }

        public void AddTile(GameObjects gameObject, int x, int y)
        {
            switch(gameObject)
            {
                case GameObjects.Ground:
                    level.AddGameObject(new Ground(level, blockWidth, new Vector2(blockWidth * x, -blockWidth * y)), x, y);
                    break;
                case GameObjects.Player:
                    level.AddGameObject(new Player(level, blockWidth, new Vector2(blockWidth * x, -blockWidth * y)), x, y);
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
                case GameObjects.Player:
                    Player.DrawIcon(gameTime, spriteBatch, new Rectangle(x, y, (int)blockWidth, (int)blockWidth));
                    break;
            }
        }

        public void Save()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(levelDir, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, level);
            stream.Close();
            Console.WriteLine("Saved.");
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

    enum GameObjects
    {
        Ground,
        Player
    }
}
