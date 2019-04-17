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

        [NonSerialized]
        Vector2 pos;
        [NonSerialized]
        Texture2D image;
        public Arrows(Level level, float blockWidth, Vector2 spawnPos) : base(level, blockWidth, spawnPos)
        {

        }

        public override Vector2 Pos
        {
            get
            {
                return pos;
            }

            set
            {
                pos = value;
            }
        }



        public override void DataValueName()
        {

        }

        public override void Initialize(Level level, float blockWidth)
        {
            Level = level;
            BlockWidth = blockWidth;
            image = LoadedContent.ArrowR;
            pos = SpawnPos * (int)blockWidth;
        }

        public override void NextDataValue()
        {

        }

        public override void PreviousDataValue()
        {

        }

        public override void Update(GameTime gameTime)
        {

        }

        public static void DrawIcon(GameTime gameTime, SpriteBatch spriteBatch, Rectangle rect)
        {
            spriteBatch.Draw(LoadedContent.ArrowR, rect, new Rectangle(0, 0, 108, 108), Color.White);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera)
        {
            spritebach.Draw(image, new Rectangle((int)Pos.X - (int)camera.X, (int)Pos.Y - (int)camera.Y, (int)BlockWidth, (int)BlockWidth), new Rectangle(0, 0, 108, 108), Color.White);
        }
    }
}
