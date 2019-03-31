using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocks
{
    class CollisionTestScreen : Screen
    {
        Texture2D block;
        PhysicsManager physicsManager;

        public CollisionTestScreen(GraphicsDevice graphicsDevice, Game1 game1, Texture2D block) : base(graphicsDevice, game1)
        {
            this.block = block;

            physicsManager = new PhysicsManager();

            Body newBody = new Body(physicsManager, false, 1, 0, 0);
            newBody.AddCollider(new RectangleCollider(newBody, CollisionGroup.Group1, new Vector2(), new Vector2(108, 108), collisionData => true));
            newBody.Pos = new Vector2(0, graphicsDevice.Viewport.Height / 2);
            newBody.Vel = new Vector2(300, 0);

            newBody = new Body(physicsManager, false, 1, 0, 0);
            newBody.AddCollider(new RectangleCollider(newBody, CollisionGroup.Group2, new Vector2(), new Vector2(108, 108), collisionData => true));
            newBody.Pos = new Vector2(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height / 2);
            newBody.Vel = new Vector2(-300, 0);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach(Body body in physicsManager.Bodies)
            {
                spriteBatch.Draw(block, new Rectangle((int)body.Pos.X, (int)body.Pos.Y, 108, 108), Color.White);
            }
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            physicsManager.Update(gameTime);
        }
    }
}
