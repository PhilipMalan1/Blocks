using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocks
{
    class Ground : GameObject
    {
        Texture2D ground;
        Rectangle floor;

        public Ground(Texture2D g,Rectangle r)
        {
            ground = g;
            floor = r;
        }

        public override void DataValueName()
        {
            throw new NotImplementedException();
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void NextDataValue()
        {
            throw new NotImplementedException();
        }

        public override void PreviousDataValue()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera)
        {
            spritebach.Draw(ground, floor, new Rectangle(0, 0, 108, 108), Color.White);
        }
        
    }
}
