using System;
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
    /// <summary>
    /// Click a block from the toolbar at the top of the screen to draw with it.
    /// Left click to draw and right click to delete.
    /// Hold left ctrl to draw objects on top of each other
    /// Click on "Select" to use the select tool (or toggle it with e).
    /// If you click on a block with the select tool, some information about the block will be in the bottom left of the screen.
    /// You can change the data value of a selected block with "," and ".".
    /// You can move a selected block by dragging with the mouse or using the arrow keys
    /// Press "Q" to save and "Esc" to save and quit.
    /// </summary>
    public class LevelEditorScreen : Screen
    {
        private Level level;
        Vector2 camera;
        float blockWidth;
        KeyboardState key, keyi;
        MouseState mouse, mousei;
        string levelDir;
        SpriteFont font;

        State state;
        GameObjectsEnum current;
        GameObject currentSelection;
        int currentSelectionX, currentSelectionY, currentSelectionLayer;
        int objectRow, objectsPerRow;

        int previousX, previousY;

        public float BlockWidth
        {
            get
            {
                return blockWidth;
            }

            set
            {
                blockWidth = value;
            }
        }

        public LevelEditorScreen(GraphicsDevice graphicsDevice, Game1 game1, string levelDir, bool load) : base(graphicsDevice, game1)
        {
            this.levelDir = levelDir;

            key = Keyboard.GetState();
            mouse = Mouse.GetState();

            blockWidth = graphicsDevice.Viewport.Height / 15;

            List<List<List<GameObject>>> levelGrid = new List<List<List<GameObject>>>();

            level = new Level(levelGrid, 0, blockWidth, graphicsDevice, ()=>
            {
                game1.SetScreen(null);
            });

            font = LoadedContent.mainMenuFont;

            camera = new Vector2(0, -graphicsDevice.Viewport.Height);

            current = 0;
            objectRow = 0;
            objectsPerRow = graphicsDevice.Viewport.Width / (int)blockWidth;

            state = State.Draw;

            if(load) Load();
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

            //draw GameObject selecter
            Array GameObjectList = Enum.GetValues(typeof(GameObjectsEnum));
            for (int x = 0; x < graphicsDevice.Viewport.Width / (int)blockWidth; x++)
            {
                if (x < GameObjectList.GetLength(0))
                    DrawIcon((GameObjectsEnum)GameObjectList.GetValue(x), gameTime, spriteBatch, x * (int)blockWidth, 0);
            }

            //draw select tool
            spriteBatch.DrawString(font, "Select", new Vector2(0, blockWidth), Color.White, 0, new Vector2(), 1f / 100 * blockWidth / 4, SpriteEffects.None, (float)DrawLayer.UI / 1000);

            //draw block description
            string str = "";
            if (state == State.Select)
            {
                if (currentSelection != null)
                    str = currentSelection.DataValueName() + " Pos: (" + currentSelectionX + ", " + currentSelectionY + ") Layer: " + currentSelectionLayer;
                else
                    str = "No object selected.";
            }
            else if (state == State.Draw)
                str = "Current Object: " + Enum.GetName(typeof(GameObjectsEnum), current);
            else if (state == State.Typeing)
                str = "Typing";
            spriteBatch.DrawString(font, str, new Vector2(0, graphicsDevice.Viewport.Height - font.MeasureString(str).Y / 100 * blockWidth / 4), Color.White, 0, new Vector2(), 1f / 100 * blockWidth / 4, SpriteEffects.None, (float)DrawLayer.UI / 1000);

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
                if (mouse.Y < blockWidth && blockX < objectsPerRow && selectedBlock < Enum.GetNames(typeof(GameObjectsEnum)).Count())
                {
                    if (mousei.LeftButton == ButtonState.Released)
                    {
                        current = (GameObjectsEnum)selectedBlock;
                        state = State.Draw;
                    }
                }
                //select tool
                else if (mouse.Y < blockWidth * 3f / 2 && mouse.X < font.MeasureString("Select").X / 100 * blockWidth / 4)
                {
                    state = State.Select;
                }
                else
                {
                    //select block
                    if (state==State.Select && mousei.LeftButton==ButtonState.Released)
                    {
                        if (level.CheckForObject(x, y))
                        {
                            int layer = level.LevelObjects[x][y].Count() - 1;
                            //if you are selecting a block that is already selected, cycle through the blocks at that location
                            int index = level.LevelObjects[x][y].IndexOf(currentSelection);
                            if (index != -1)
                            {
                                if (index > 0)
                                    layer = index - 1;
                            }
                            currentSelection = level.LevelObjects[x][y][layer];
                            currentSelectionX = x;
                            currentSelectionY = y;
                            currentSelectionLayer = layer;
                        }
                        else
                            currentSelection = null;
                    }
                    //draw
                    else if(state==State.Draw)
                    {
                        //draw block if there isn't a block there
                        if (!level.CheckForObject(x, y))
                            AddTile(current, x, y);
                        //draw a block if you are holding left ctrl and (left click wasn't down last frame or the mouse moved to a different block)
                        if (key.IsKeyDown(Keys.LeftControl) && (mousei.LeftButton == ButtonState.Released || x != previousX || y != previousY))
                            AddTile(current, x, y);
                    }
                }
            }

            //toggle select tool
            if(state!=State.Typeing && key.IsKeyDown(Keys.E) && keyi.IsKeyUp(Keys.E))
            {
                if (state == State.Select)
                    state = State.Draw;
                else
                    state = State.Select;
            }

            //delet blocks if right click wasn't held for the previous frame and the mouse didn't move to a different block
            if (mouse.RightButton == ButtonState.Pressed && (mousei.RightButton == ButtonState.Released || x != previousX || y != previousY))
            {
                level.RemoveGameObject(x, y);
            }

            //move camera
            if(state!=State.Typeing)
            {
                float cameraSpeed = blockWidth;
                if (key.IsKeyDown(Keys.D)) camera.X += cameraSpeed;
                if (key.IsKeyDown(Keys.A)) camera.X -= cameraSpeed;
                if (key.IsKeyDown(Keys.W)) camera.Y -= cameraSpeed;
                if (key.IsKeyDown(Keys.S)) camera.Y += cameraSpeed;
                if (camera.X < 0) camera.X = 0;
                if (camera.Y > -graphicsDevice.Viewport.Height) camera.Y = -graphicsDevice.Viewport.Height;
            }

            //change data value
            if(state==State.Select && currentSelection!=null)
            {
                if (key.IsKeyDown(Keys.OemComma) && keyi.IsKeyUp(Keys.OemComma))
                    currentSelection.PreviousDataValue();
                if (key.IsKeyDown(Keys.OemPeriod) && keyi.IsKeyUp(Keys.OemPeriod))
                    currentSelection.NextDataValue();
            }

            //move selected item
            try
            {
                if (currentSelection != null)
                {
                    if (state==State.Select && mouse.LeftButton == ButtonState.Pressed && (x != previousX || y != previousY))
                    {
                        if(MoveTile(currentSelection, currentSelectionX, currentSelectionY, x, y))
                        {
                            currentSelectionX = x;
                            currentSelectionY = y;
                        }
                    }
                    if (key.IsKeyDown(Keys.Left) && keyi.IsKeyUp(Keys.Left))
                    {
                        if (MoveTile(currentSelection, currentSelectionX, currentSelectionY, currentSelectionX-1, currentSelectionY))
                        {
                            currentSelectionX--;
                        }
                    }
                    if (key.IsKeyDown(Keys.Right) && keyi.IsKeyUp(Keys.Right))
                    {
                        if (MoveTile(currentSelection, currentSelectionX, currentSelectionY, currentSelectionX + 1, currentSelectionY))
                        {
                            currentSelectionX++;
                        }
                    }
                    if (key.IsKeyDown(Keys.Up) && keyi.IsKeyUp(Keys.Up))
                    {
                        if (MoveTile(currentSelection, currentSelectionX, currentSelectionY, currentSelectionX, currentSelectionY + 1))
                        {
                            currentSelectionY++;
                        }
                    }
                    if (key.IsKeyDown(Keys.Down) && keyi.IsKeyUp(Keys.Down))
                    {
                        if (MoveTile(currentSelection, currentSelectionX, currentSelectionY, currentSelectionX, currentSelectionY - 1))
                        {
                            currentSelectionY--;
                        }
                    }
                }
            }
            catch(Exception) { }

            //If selecting floating text, edit text
            if (currentSelection is FloatingText)
            {
                FloatingText floatingText = (FloatingText)currentSelection;
                if (state == State.Typeing)
                {
                    Keys currentKey;
                    int i = 0;
                    do
                    {
                        if(i>=key.GetPressedKeys().Count())
                        {
                            currentKey = Keys.None;
                            break;
                        }
                        currentKey = key.GetPressedKeys()[i];
                        i++;
                    } while (currentKey == Keys.LeftShift || currentKey == Keys.RightShift || currentKey==Keys.Enter);

                    if (keyi.IsKeyDown(currentKey))
                        currentKey = Keys.None;

                    if (currentKey != Keys.None)
                    {
                        switch (currentKey)
                        {
                            case Keys.Back:
                                if(floatingText.Text.Count()>0)
                                    floatingText.Text = floatingText.Text.Substring(0, floatingText.Text.Count() - 1);
                                break;
                            case Keys.Space:
                                floatingText.Text += ' ';
                                break;
                            case Keys.D1:
                                if (key.IsKeyUp(Keys.LeftShift) && key.IsKeyUp(Keys.RightShift))
                                    floatingText.Text += '1';
                                else
                                    floatingText.Text += '!';
                                break;
                            case Keys.OemComma:
                                if (key.IsKeyUp(Keys.LeftShift) && key.IsKeyUp(Keys.RightShift))
                                    floatingText.Text += ',';
                                else
                                    floatingText.Text += '<';
                                break;
                            case Keys.OemPeriod:
                                if (key.IsKeyUp(Keys.LeftShift) && key.IsKeyUp(Keys.RightShift))
                                    floatingText.Text += '.';
                                else
                                    floatingText.Text += '>';
                                break;
                            case Keys.OemQuestion:
                                if (key.IsKeyUp(Keys.LeftShift) && key.IsKeyUp(Keys.RightShift))
                                    floatingText.Text += '/';
                                else
                                    floatingText.Text += '?';
                                break;
                            case Keys.OemSemicolon:
                                if (key.IsKeyUp(Keys.LeftShift) && key.IsKeyUp(Keys.RightShift))
                                    floatingText.Text += ';';
                                else
                                    floatingText.Text += ':';
                                break;
                            default:
                                if (key.IsKeyUp(Keys.LeftShift) && key.IsKeyUp(Keys.RightShift))
                                    floatingText.Text += currentKey.ToString().ToLower();
                                else
                                    floatingText.Text += currentKey.ToString().ToUpper();
                                break;
                        }
                    }
                }

                if (key.IsKeyDown(Keys.Enter) && keyi.IsKeyUp(Keys.Enter))
                {
                    if (state == State.Select)
                        state = State.Typeing;
                    else if (state == State.Typeing)
                        state = State.Select;
                }
            }

            //save
            if (state!=State.Typeing && (key.IsKeyDown(Keys.Escape) || (key.IsKeyDown(Keys.Q) && keyi.IsKeyUp(Keys.Q))))
                Save();

            //test level
            if (state != State.Typeing && key.IsKeyDown(Keys.T) && keyi.IsKeyUp(Keys.T))
                game1.SetScreen(new Screens.LevelTestScreen(graphicsDevice, game1, this, level));

            //set previous x and y
            previousX = x;
            previousY = y;
        }

        public bool MoveTile(GameObject gameObject, int x, int y, int toX, int toY)
        {
            if (toX >= 0 && toY >= 0)
            {
                level.LevelObjects[x][y].Remove(gameObject);
                level.AddGameObject(gameObject, toX, toY);
                currentSelection.Move(toX, toY);
                return true;
            }
            else
                return false;
        }

        public void AddTile(GameObjectsEnum gameObject, int x, int y)
        {
            switch(gameObject)
            {
                case GameObjectsEnum.Ground:
                    level.AddGameObject(new Ground(level, blockWidth, new Vector2(x, -y)), x, y);
                    break;
                case GameObjectsEnum.fallingPlatform:
                    level.AddGameObject(new FallingPlatform(level, blockWidth, new Vector2(x, -y)), x, y);
                    break;
                case GameObjectsEnum.Arrows:
                    level.AddGameObject(new Arrows(level, blockWidth, new Vector2(x, -y)), x, y);
                    break;
                case GameObjectsEnum.Player:
                    level.AddGameObject(new Player(level, blockWidth, new Vector2(x, -y)), x, y);
                    break;
                case GameObjectsEnum.Spikes:
                    level.AddGameObject(new Spikes(level, blockWidth, new Vector2(x, -y)), x, y);
                    break;
                case GameObjectsEnum.Block:
                    level.AddGameObject(new Block(level, blockWidth, new Vector2(x, -y)), x, y);
                    break;
                case GameObjectsEnum.Button:
                    level.AddGameObject(new Button(level, blockWidth, new Vector2(x, -y)), x, y);
                    break;
                case GameObjectsEnum.FloatingText:
                    level.AddGameObject(new FloatingText(level, blockWidth, new Vector2(x, -y)), x, y);
                    break;
                case GameObjectsEnum.Goal:
                    level.AddGameObject(new Goal(level, blockWidth, new Vector2(x, -y)), x, y);
                    break;
            }
        }

        public void DrawIcon(GameObjectsEnum gameObject, GameTime gameTime, SpriteBatch spriteBatch, int x, int y)
        {
            switch(gameObject)
            {
                case GameObjectsEnum.Ground:
                    Ground.DrawIcon(gameTime, spriteBatch, new Rectangle(x, y, (int)blockWidth, (int)blockWidth));
                    break;
                case GameObjectsEnum.fallingPlatform:
                    FallingPlatform.DrawIcon(gameTime, spriteBatch, new Rectangle(x, y, (int)blockWidth, (int)blockWidth));
                    break;
                case GameObjectsEnum.Arrows:
                    Arrows.DrawIcon(gameTime, spriteBatch, new Rectangle(x, y, (int)blockWidth, (int)blockWidth));
                    break;
                case GameObjectsEnum.Player:
                    Player.DrawIcon(gameTime, spriteBatch, new Rectangle(x, y, (int)blockWidth, (int)blockWidth));
                    break;
                case GameObjectsEnum.Spikes:
                    Spikes.DrawIcon(gameTime, spriteBatch, new Rectangle(x, y, (int)blockWidth, (int)blockWidth));
                    break;
                case GameObjectsEnum.Block:
                    Block.DrawIcon(gameTime, spriteBatch, new Rectangle(x, y, (int)blockWidth, (int)blockWidth));
                    break;
                case GameObjectsEnum.Button:
                    Button.DrawIcon(gameTime, spriteBatch, new Rectangle(x, y, (int)blockWidth, (int)blockWidth), blockWidth);
                    break;
                case GameObjectsEnum.FloatingText:
                    FloatingText.DrawIcon(gameTime, spriteBatch, new Rectangle(x, y, (int)blockWidth, (int)blockWidth));
                    break;
                case GameObjectsEnum.Goal:
                    Goal.DrawIcon(gameTime, spriteBatch, new Rectangle(x, y, (int)blockWidth, (int)blockWidth));
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
            level.Initialize(blockWidth, graphicsDevice, null);
        }

        public enum GameObjects
        {
            Ground,
            Player,
            Spikes
        }
        private enum State
        {
            Draw,
            Select,
            Typeing
        }
    }
}
