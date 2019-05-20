using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Blocks
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Screen screen;
        SpriteFont font1, font2;
        bool spacePressed = false;
        int hits;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;
            Window.Title = "Blocks";
            graphics.PreferredBackBufferWidth = 1730;
            graphics.PreferredBackBufferHeight = 970;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            LoadedContent.LoadContent(Content);
            screen = new LevelEditorScreen(GraphicsDevice, this, @"Content/Levels/level 4 2.dat", true);
            //screen = new Start_Menu(GraphicsDevice, this);
            //screen = new LevelEditorScreen(GraphicsDevice, this, @"Content/Levels/philipLevel 7.dat", true);
            screen = new Start_Menu(GraphicsDevice, this);
            hits = 0;
            base.Initialize();
        }

        public bool IntroDone
        {
            get { return introDone; }
        }
        bool introDone = true;


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font1 = Content.Load<SpriteFont>("fonts/SpriteFont1");
            font2 = Content.Load<SpriteFont>("fonts/SpriteFont2");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            screen.Update(gameTime);

            // Allows the game to exit
            KeyboardState keyboard = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            if (hits < 1&& keyboard.IsKeyDown(Keys.Space) && !spacePressed)
            {
                spacePressed = true;
                hits++;
            }
            if (hits < 2 && keyboard.IsKeyDown(Keys.Space) && !spacePressed)
            {
                spacePressed = true;
                hits++;
            }
            if (hits < 3 && keyboard.IsKeyDown(Keys.Space) && !spacePressed)
            {
                spacePressed = true;
                hits++;
            }
            if (hits < 3 && keyboard.IsKeyUp(Keys.Space)&&spacePressed)
            {
                spacePressed = false;
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            screen.Draw(gameTime, spriteBatch);
            spriteBatch.Begin();
            if (!introDone)
            {
                GraphicsDevice.Clear(Color.Black);
                if (hits < 1)
                {
                    spriteBatch.DrawString(font1, "Block, Triangle, Trapezoid, and Circle.... The four shapes \nall lived in harmony until the Blocks attacked", new Vector2(20, 50), Color.White);
                }
                else if (hits < 2)
                {
                    spriteBatch.DrawString(font1, "Block-Land was officially at war with Circle-Land which resulted in blocks becoming illegal in \nCircle land. One day, a circle, ironically named Circle was selling blocks to support his family", new Vector2(20, 50), Color.White);
                }
                else if (hits < 3)
                {
                    spriteBatch.DrawString(font1, "He was then put in a prison inside a mountain with multiple empty cells between\nhim and the outer world. This is the beginning of his journey to get back to his family.", new Vector2(20, 50), Color.White);
                }
                else
                introDone = true;
                spriteBatch.DrawString(font2, "Press Space to Continue >>>", new Vector2(1600, 875), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void SetScreen(Screen screen)
        {
            this.screen = screen;
        }

        public void Intro()
        {
            introDone = false;
        }
    }
}
