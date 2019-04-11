using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocks
{
    public class Block : GameObject
    {
        private Level level;
        Texture2D image;
        Body body;

        public Block(Level level, float blockWidth, Vector2 spawnPos) : base(level, blockWidth, spawnPos)
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

        public static void DrawIcon(GameTime gameTime, SpriteBatch spriteBatch, Rectangle rect)
        {
            spriteBatch.Draw(LoadedContent.block, rect, new Rectangle(0, 0, 108, 108), Color.White);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera)
        {
            spritebach.Draw(image, new Rectangle((int)(Pos.X - camera.X), (int)(Pos.Y - camera.Y), (int)BlockWidth, (int)BlockWidth), new Rectangle(0, 0, 108, 108), Color.White);
        }

        public override void Initialize(Level level, float blockWidth)
        {
            this.level = level;
            BlockWidth = blockWidth;
            image = LoadedContent.block;

            body = new Body(level.PhysicsMangager, false, 1, blockWidth * 50, 1);
            body.Pos = SpawnPos * (int)blockWidth;
            body.Colliders.Add(new RectangleCollider(body, CollisionGroup.Player, new Vector2(), new Vector2(blockWidth, blockWidth), collisionData => true));
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
