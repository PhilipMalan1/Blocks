using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    [Serializable]
    public abstract class Sprite
    {
        [NonSerialized]
        bool isLoaded;

        public abstract Vector2 Pos
        {
            get;

            set;
        }

        public bool IsLoaded
        {
            get
            {
                return isLoaded;
            }

            set
            {
                isLoaded = value;
            }
        }
        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera);
          
       
    }
}
