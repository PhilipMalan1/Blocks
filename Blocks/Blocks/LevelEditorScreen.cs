using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocks
{
    class LevelEditorScreen : Screen
    {
        private List<List<LinkedList<GameObject>>> level;
            
        public LevelEditorScreen(GraphicsDevice graphicsDevice, Game1 game1) : base(graphicsDevice, game1)
        {
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
