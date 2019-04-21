using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocks
{
    [Serializable]
    public abstract class GameObject : Sprite
    {
        [NonSerialized]
        private Level level;
        private float blockWidth;
        private Vector2 spawnPos;

        public GameObject(Level level, float blockWidth, Vector2 spawnPos)
        {
            this.spawnPos = spawnPos;
            Initialize(level, blockWidth);
        }

        public abstract Vector2 Vel
        {
            get;
            set;
        }

        virtual public Rectangle LoadRect
        {
            get
            {
                return new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)(blockWidth * 2), (int)(blockWidth * 2));
            }
        }

        internal Level Level
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
        public abstract string DataValueName();
        public abstract void Initialize(Level level, float blockWidth);

        abstract public override void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera);

        abstract public override void Update(GameTime gameTime);

        abstract public void Load();
        abstract public void Unload();

        virtual public void Move(int x, int y)
        {
            SpawnPos = new Vector2(x, -y);
            Initialize(level, blockWidth);
        }
    }
}
