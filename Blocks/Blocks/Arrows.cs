using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    class Arrows : Sprite
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


        public Arrows(bool up,Rectangle r)
        {
            if(up)
            arrowTex = LoadedContent.ArrowUR;
            else
                arrowTex = LoadedContent.ArrowR;
            arrowRec = r;
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
    }
}
