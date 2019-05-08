using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Blocks
{
    class Start_Menu : Screen
    {
        Texture2D menuBackText;
        Rectangle menuBackRec;
        Texture2D buttonTex;
        int hits;
        Rectangle playRec;
        bool play;
        bool spacePressed = false;
        Rectangle exitRec;
        SpriteFont font;
        public Start_Menu(GraphicsDevice graphicsDevice, Game1 game1) : base(graphicsDevice, game1)
        {
            menuBackRec = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
            menuBackText = LoadedContent.mainMenuBackground;
            buttonTex = LoadedContent.mainMenuButton;
            playRec = new Rectangle(0,200,400,200);
            exitRec = new Rectangle(0, 410, 400, 200);
            hits = 0;
            play = false;
            font = LoadedContent.mainMenuFont;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(menuBackText,menuBackRec,Color.White);
            spriteBatch.Draw(buttonTex, playRec, Color.White);
            spriteBatch.Draw(buttonTex, exitRec, Color.White);
            spriteBatch.DrawString(font, "Play", new Vector2(playRec.X + 8,playRec.Y + 5), Color.Black);
            spriteBatch.DrawString(font, "Exit", new Vector2(exitRec.X + 8,exitRec.Y + 5), Color.Black);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState md = Mouse.GetState();
            if (md.LeftButton == ButtonState.Pressed && md.X > exitRec.X && md.X < exitRec.X + exitRec.Width && md.Y > exitRec.Y && md.Y < exitRec.Y + exitRec.Height)
                game1.Exit();
            if (md.LeftButton == ButtonState.Pressed && md.X > playRec.X && md.X < playRec.X + playRec.Width && md.Y > playRec.Y && md.Y < playRec.Y + playRec.Height)
            {
                play = true;
            }
            if (hits < 1 && keyboard.IsKeyDown(Keys.Space)&&!spacePressed)
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
            if (play&& hits < 3)
                game1.Intro();
            else if(play)
                game1.SetScreen(new GameScreen(graphicsDevice, game1, @"Content/Levels/Level 1.dat"));
            if (hits < 3 && keyboard.IsKeyUp(Keys.Space)&& spacePressed)
            {
                spacePressed = false;
            }
            

        }
    }
}
