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
        public Start_Menu(GraphicsDevice graphicsDevice, Game1 game1) : base(graphicsDevice, game1)
        {

            menuBackRec = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
