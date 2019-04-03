using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocks
{
    abstract class GameObject : Sprite
    {
        protected int dataValue;

        public abstract void NextDataValue();
        public abstract void PreviousDataValue();
        public abstract void DataValueName();
        public abstract void Initialize();
        abstract public void DrawIcon(GameTime gameTime, SpriteBatch spriteBatch, Rectangle rect);

        abstract public override void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera);

        abstract public override void Update(GameTime gameTime);
    }
}
