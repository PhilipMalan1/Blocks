using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocks
{
    [Serializable]
    class FloatingText : GameObject
    {
        string text;

        [NonSerialized]
        Vector2 pos;
        [NonSerialized]
        SpriteFont font;

        public FloatingText(Level level, float blockWidth, Vector2 spawnPos) : base(level, blockWidth, spawnPos)
        {
            text = "hi";
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
                return new Vector2();
            }

            set
            {
            }
        }

        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }

        public override string DataValueName()
        {
            return "Object: Floating Text Data Value: N/A";
        }

        public static void DrawIcon(GameTime gameTime, SpriteBatch spriteBatch, Rectangle rect)
        {
            float scale = (float)rect.Width / 40 / 2;
            spriteBatch.DrawString(LoadedContent.mainMenuFont, "T", new Vector2(rect.X, rect.Y), Color.White, 0, new Vector2(), scale, SpriteEffects.None, (float)DrawLayer.UI / 1000);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera)
        {
            float scale = BlockWidth / 90 / 2;
            Vector2 strSize = font.MeasureString(text)*scale;
            spritebach.DrawString(font, text, new Vector2(pos.X + BlockWidth / 2 - strSize.X / 2 - camera.X, pos.Y + BlockWidth / 2 - strSize.Y / 2 - camera.Y), Color.Black, 0, new Vector2(), scale, SpriteEffects.None, (float)DrawLayer.FloatingText / 1000);
        }

        public override void Initialize(Level level, float blockWidth)
        {
            Level = level;
            BlockWidth = blockWidth;
            font = LoadedContent.mainMenuFont;
            pos = SpawnPos * blockWidth;
        }

        public override void Load()
        {
        }

        public override void NextDataValue()
        {
        }

        public override void PreviousDataValue()
        {
        }

        public override void Unload()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
