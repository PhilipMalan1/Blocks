using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocks
{
    [Serializable]
    class Ground : GameObject
    {
        [NonSerialized]
        Body body;
        [NonSerialized]
        Texture2D image;

        public Ground(Level level, float blockWidth, Vector2 spawnPos) : base(level, blockWidth, spawnPos)
        {
            
        }

        public override Vector2 Pos
        {
            get
            {
                return body.Pos;
            }

            set
            {
                body.Pos = value;
            }
        }

        

        public override void DataValueName()
        {
            
        }

        public override void Initialize(Level level, float blockWidth)
        {
            Level = level;
            BlockWidth = blockWidth;
            image = LoadedContent.ground;
            body = new Body(level.PhysicsMangager, true, 1, 0, 0);
            body.Pos = SpawnPos;
            body.AddCollider(new RectangleCollider(body, CollisionGroup.Ground, new Vector2(), new Vector2(blockWidth, blockWidth), collisionData => true));
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

        public override void DrawIcon(GameTime gameTime, SpriteBatch spriteBatch, Rectangle rect)
        {
            spriteBatch.Draw(image, rect, new Rectangle(0, 0, 108, 108), Color.White);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera)
        {
            spritebach.Draw(image, new Rectangle((int)Pos.X-(int)camera.X, (int)Pos.Y-(int)camera.Y, (int)BlockWidth, (int)BlockWidth), new Rectangle(0, 0, 108, 108), Color.White);
        }
        
    }
}
