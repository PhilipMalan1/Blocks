using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocks
{
    abstract class GameObject : Sprite
    {
        private Texture2D current;
        private int dataValue;
        public Texture2D Current
        {
            get
            {
                return current;
            }
            
        }

        public int DataValue
        {
            get
            {
                return dataValue;
            }

            set
            {
                dataValue = value;
            }
        }

        public abstract void NextDataValue();
        public abstract void PreviousDataValue();
        public abstract void DataValueName();
        public abstract void Initialize();

        abstract public override void Draw(GameTime gameTime);

        abstract public override void Update(GameTime gameTime);
    }
}
