using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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
        LoadedContent l;
        public Start_Menu(GraphicsDevice graphicsDevice, Game1 game1) : base(graphicsDevice, game1)
        {
            l = new LoadedContent();
            menuBackRec = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
            menuBackText = LoadedContent.mainMenuBackground;
            buttonTex = LoadedContent.mainMenuButton;
            playRec = new Rectangle(0,200,200,50);
            exitRec = new Rectangle(0, 300, 200, 50);
            font = LoadedContent.mainMenuFont;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(menuBackText,menuBackRec,Color.White);
            spriteBatch.Draw(buttonTex, playRec, Color.White);
            spriteBatch.Draw(buttonTex, exitRec, Color.White);
            spriteBatch.DrawString(font, "Play", new Vector2(playRec.X + 10,playRec.Y + 10), Color.Black);
            spriteBatch.DrawString(font, "Exit", new Vector2(exitRec.X + 10,exitRec.Y + 10), Color.Black);
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
