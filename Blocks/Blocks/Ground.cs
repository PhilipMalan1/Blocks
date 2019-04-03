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
        Vector2 Pos;
        Texture2D image;
        float blockWidth;

        public Vector2 Pos1
        {
            get
            {
                return Pos;
            }

            set
            {
                Pos = value;
            }
        }

        public Ground(Vector2 pos, float blockWidth)
        {
            Pos = pos;
            this.blockWidth = blockWidth;

            Initialize();
        }

        public override void DataValueName()
        {
            throw new NotImplementedException();
        }

        public override void Initialize()
        {
            image = LoadedContent.ground;
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
            spritebach.Draw(image, new Rectangle((int)Pos.X-(int)camera.X, (int)Pos.Y-(int)camera.Y, (int)blockWidth, (int)blockWidth), new Rectangle(0, 0, 108, 108), Color.White);
        }
        
    }
}
