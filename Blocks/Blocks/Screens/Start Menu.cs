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
        Rectangle playRec;
        Rectangle exitRec;
        SpriteFont font;
        public Start_Menu(GraphicsDevice graphicsDevice, Game1 game1) : base(graphicsDevice, game1)
        {
            menuBackRec = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
            menuBackText = LoadedContent.mainMenuBackground;
            buttonTex = LoadedContent.mainMenuButton;
            playRec = new Rectangle(0,200,400,200);
            exitRec = new Rectangle(0, 410, 400, 200);
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
            MouseState md = Mouse.GetState();
            if (md.LeftButton == ButtonState.Pressed && md.X > exitRec.X && md.X < exitRec.X + exitRec.Width && md.Y > exitRec.Y && md.Y < exitRec.Y + exitRec.Height)
                game1.Exit();
            if (md.LeftButton == ButtonState.Pressed && md.X > playRec.X && md.X < playRec.X + playRec.Width && md.Y > playRec.Y && md.Y < playRec.Y + playRec.Height)
                game1.SetScreen(new GameScreen(graphicsDevice,game1, @"Content/Levels/"+LevelManager.firstLevel()));
        }
    }
}
