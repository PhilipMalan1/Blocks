using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    class Arrows : GameObject
    {
        // U = Up
        // R = Right

        /*
             /\     /\
            /  \___/  \
           |           |
           |   O  O    |    
           |    w      |
           |           |

         */
         
        Texture2D arrowTex;
        Rectangle arrowRec;

        public Arrows(Level level, float blockWidth, Vector2 spawnPos) : base(level, blockWidth, spawnPos)
        {
        }

        public override Vector2 Pos
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void Update(GameTime gameTime) { }

        public override void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera)
        {
            spritebach.Begin();
            spritebach.Draw(arrowTex, arrowRec, Color.White);
            spritebach.End();
        }

        public override void NextDataValue()
        {
            throw new NotImplementedException();
        }

        public override void PreviousDataValue()
        {
            throw new NotImplementedException();
        }

        public override void DataValueName()
        {
            throw new NotImplementedException();
        }

        public override void Initialize(Level level, float blockWidth)
        {
            throw new NotImplementedException();
        }
    }
}
