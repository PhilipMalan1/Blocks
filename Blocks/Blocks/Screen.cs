using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    public abstract class Screen
    {
        protected GraphicsDevice graphicsDevice;
        protected Game1 game1;

        public Screen(GraphicsDevice graphicsDevice, Game1 game1)
        {
            this.graphicsDevice = graphicsDevice;
            this.game1 = game1;
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);
    }
}
