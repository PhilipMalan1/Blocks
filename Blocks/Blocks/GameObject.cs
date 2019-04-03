using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocks
{
    [Serializable]
    abstract class GameObject : Sprite
    {
        [NonSerialized]
        private Level level;
        private float blockWidth;
        private Vector2 spawnPos;

        protected GameObject(Level level, float blockWidth, Vector2 spawnPos)
        {
            this.spawnPos = spawnPos;
            Initialize(level, blockWidth);
        }

        protected int dataValue;

        protected Level Level
        {
            get
            {
                return level;
            }

            set
            {
                level = value;
            }
        }

        protected float BlockWidth
        {
            get
            {
                return blockWidth;
            }

            set
            {
                blockWidth = value;
            }
        }

        protected Vector2 SpawnPos
        {
            get
            {
                return spawnPos;
            }

            set
            {
                spawnPos = value;
            }
        }

        public abstract void NextDataValue();
        public abstract void PreviousDataValue();
        public abstract void DataValueName();
        public abstract void Initialize(Level level, float blockWidth);
        abstract public void DrawIcon(GameTime gameTime, SpriteBatch spriteBatch, Rectangle rect);

        abstract public override void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera);

        abstract public override void Update(GameTime gameTime);
    }
}
