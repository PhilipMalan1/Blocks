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
        List<Body> bodies;

        public CollisionTestScreen(GraphicsDevice graphicsDevice, Game1 game1, Texture2D block) : base(graphicsDevice, game1)
        {
            this.block = block;

            bodies = new List<Body>();

            Body newBody = new Body(false, 1, 0, 0);
            newBody.AddCollider(new RectangleCollider(newBody, new Vector2(), new Vector2(108, 108), collisionData => { }));
            newBody.Pos = new Vector2(0, graphicsDevice.Viewport.Height / 2);
            newBody.Vel = new Vector2(5, 0);

            bodies.Add(newBody);

            newBody = new Body(false, 1, 0, 0);
            newBody.AddCollider(new RectangleCollider(newBody, new Vector2(), new Vector2(108, 108), collisionData => { }));
            newBody.Pos = new Vector2(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height / 2-100);
            newBody.Vel = new Vector2(-5, 0);

            bodies.Add(newBody);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach(Body body in bodies)
            {
                spriteBatch.Draw(block, new Rectangle((int)body.Pos.X, (int)body.Pos.Y, 108, 108), Color.White);
            }
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            bodies[0].Update();
            bodies[1].Update();
            CollisionData collisionData = bodies[0].Colliders[0].CheckCollision(bodies[1].Colliders[0]);
            Collider.ResolveCollision(collisionData);
        }
    }
}
