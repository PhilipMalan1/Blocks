using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        KeyboardState kb;
        int rotation;
        int roting;
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

        public override Vector2 Vel
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
        

        public override void Initialize(Level level, float blockWidth)
        {
            Level = level;
            BlockWidth = blockWidth;
            image = LoadedContent.ArrowR;
            pos = SpawnPos * (int)blockWidth;
            rotation = 0;
            roting = 0;
        }

        public override void NextDataValue()
        {
            rotation++;
        }

        public override void PreviousDataValue()
        {
        }
        public override void Update(GameTime gameTime)
        {
            switch(rotation)
            {
                case (0):
                    {
                        roting = 0;
                        break;
                    }
                case (1):
                    {
                        roting = 90;
                        break;
                    }
                case (2):
                    {
                        roting = 180;
                        break;
                    }
                case (3):
                    {
                        roting = 270;
                        break;
                    }
            }
        }

        public static void DrawIcon(GameTime gameTime, SpriteBatch spriteBatch, Rectangle rect)
        {
            spriteBatch.Draw(LoadedContent.ArrowR, rect, new Rectangle(0, 0, 108, 108), Color.White,0,new Vector2(),SpriteEffects.None,(float)DrawLayer.UI/1000);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera)
        {
            spritebach.Draw(image, new Rectangle((int)Pos.X - (int)camera.X, (int)Pos.Y - (int)camera.Y, (int)BlockWidth, (int)BlockWidth), new Rectangle(0, 0, 108, 108), Color.White,MathHelper.ToRadians( rotation), new Vector2(), SpriteEffects.None, (float)DrawLayer.Arrows / 1000);
        }

        public override void Load()
        {
            image = LoadedContent.ArrowR;
        }

        public override void Unload()
        {
        }

        public override string DataValueName()
        {
            throw new NotImplementedException();
        }
    }
}
