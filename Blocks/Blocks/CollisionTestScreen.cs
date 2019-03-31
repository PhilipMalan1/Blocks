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
        Texture2D block, player;
        PhysicsManager physicsManager;

        public CollisionTestScreen(GraphicsDevice graphicsDevice, Game1 game1, Texture2D block, Texture2D player) : base(graphicsDevice, game1)
        {
            this.block = block;
            this.player = player;

            physicsManager = new PhysicsManager();

            Body newBody = new Body(physicsManager, true, 1, 0, 1);
            newBody.AddCollider(new RectangleCollider(newBody, CollisionGroup.Group1, new Vector2(), new Vector2(108, 108), collisionData => true));
            newBody.Pos = new Vector2(graphicsDevice.Viewport.Width/2, graphicsDevice.Viewport.Height-50);
            newBody.Vel = new Vector2(0, 0);

            newBody = new Body(physicsManager, false, 1, 20, 1);
            newBody.AddCollider(new CircleCollider(newBody, CollisionGroup.Group2, new Vector2(108/2, 108/2), 108/2, collisionData => true));
            newBody.Pos = new Vector2(graphicsDevice.Viewport.Width/2+100, graphicsDevice.Viewport.Height/2);
            newBody.Vel = new Vector2(-10, 0);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach(Body body in physicsManager.Bodies)
            {
                if(body.Colliders[0] is RectangleCollider) spriteBatch.Draw(block, new Rectangle((int)body.Pos.X, (int)body.Pos.Y, 108, 108), Color.White);
                else spriteBatch.Draw(player, new Rectangle((int)body.Pos.X, (int)body.Pos.Y, 108, 108), new Rectangle(0, 108, 108, 108), Color.White);
            }
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            physicsManager.Update(gameTime);
        }
    }
}
