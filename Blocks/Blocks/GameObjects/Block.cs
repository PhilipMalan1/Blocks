using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocks.GameObjects
{
    class Block : GameObject
    {
        private Level level;
        private float blockWidth;
        Texture2D image;
        Body body;

        public Block(Level level, float blockWidth, Vector2 spawnPos) : base(level, blockWidth, spawnPos)
        {
        }

        public override Vector2 Pos
        {
            get
            {
                
            }

            set
            {
                
            }
        }

        public override void DataValueName()
        {
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera)
        {
            spritebach.Draw()
        }

        public override void Initialize(Level level, float blockWidth)
        {
            this.level = level;
            this.blockWidth = blockWidth;
            image=LoadedContent.

            body = new Body(level.PhysicsMangager, false, 1, blockWidth * 50, 1);
            body.Pos = SpawnPos * blockWidth;
            body.Colliders.Add(new RectangleCollider(body, CollisionGroup.Player, new Vector2(), new Vector2(BlockWidth, BlockWidth), collisionData => true));
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
    }
}
