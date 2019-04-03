using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocks
{
    class Ground : GameObject
    {
        PhysicsManager physicsManager;
        float blockWidth;

        Body body;
        Texture2D image;
        Vector2 spawnPos;

        public Vector2 Pos
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

        public Ground(PhysicsManager physicsManager, float blockWidth, Vector2 pos)
        {
            this.physicsManager = physicsManager;
            this.blockWidth = blockWidth;
            spawnPos = pos;

            Initialize();
        }

        public override void DataValueName()
        {
            
        }

        public override void Initialize()
        {
            image = LoadedContent.ground;
            body = new Body(physicsManager, true, 1, 0, 0);
            body.Pos = spawnPos;
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
            spritebach.Draw(image, new Rectangle((int)Pos.X-(int)camera.X, (int)Pos.Y-(int)camera.Y, (int)blockWidth, (int)blockWidth), new Rectangle(0, 0, 108, 108), Color.White);
        }
        
    }
}
