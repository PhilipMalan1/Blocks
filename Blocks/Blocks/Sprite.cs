using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    abstract class Sprite
    {
        Vector2 coordinates;
        Boolean isLoaded;

        public Vector2 Coordinates
        {
            get
            {
                return coordinates;
            }

            set
            {
                coordinates = value;
            }
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
