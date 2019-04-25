using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    /// <summary>
    ///      /\     /\
    ///     /  \___/  \
    ///    |           |
    ///    |   O  O    |
    ///    |    w      |
    ///    |           |
    /// </summary>
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
        public Arrows(Level level, float blockWidth, Vector2 spawnPos) : base(level, blockWidth, spawnPos)
        {
            rotation = 0;
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
        }

        public override void NextDataValue()
        {
            rotation++;
            rotation %= 8;
        }

        public override void PreviousDataValue()
        {
            rotation--;
            rotation %= 8;
        }
        public override void Update(GameTime gameTime)
        {

        }

        public static void DrawIcon(GameTime gameTime, SpriteBatch spriteBatch, Rectangle rect)
        {
            spriteBatch.Draw(LoadedContent.ArrowR, rect, new Rectangle(0, 0, 108, 108), Color.White,0,new Vector2(),SpriteEffects.None,(float)DrawLayer.UI/1000);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera)
        {
            Vector2 origin = new Vector2(image.Width / 2, -image.Width / 2 + image.Height);
            spritebach.Draw(image, new Rectangle((int)Pos.X - (int)camera.X, (int)Pos.Y - (int)camera.Y, (int)BlockWidth, (int)BlockWidth), new Rectangle(0, 0, 108, 108), Color.White, (float)Math.PI / 4 * rotation, origin, SpriteEffects.None, (float)DrawLayer.Arrows / 1000);
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
            return "Object: Arrows DataValue: rotation";

        }
    }
}
