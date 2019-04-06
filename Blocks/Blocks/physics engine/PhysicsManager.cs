using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    public class PhysicsManager
    {
        List<Body> bodies;

        public PhysicsManager()
        {
            bodies = new List<Body>();
        }

        /// <summary>
        /// Checks if two colliders can collide based on collision groups. Add it to a screen by making a new Physics manager and running it's update method every frame.
        /// Every time you add a body object, it will automatically add itself to the physics manager that you give to it. Bodies won't collide unless you add a collider.
        /// </summary>
        /// <param name="collider1"></param>
        /// <param name="collider2"></param>
        /// <returns></returns>
        public bool CanCollide(Collider collider1, Collider collider2)
        {
            return true;
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;

            foreach(Body body in bodies)
            {
                body.Update(dt);
            }

            for (int i = 0; i < bodies.Count()-1; i++)
            {
                for (int j = i+1; j < bodies.Count(); j++)
                {
                    foreach(Collider collider1 in bodies[i].Colliders)
                    {
                        foreach(Collider collider2 in bodies[j].Colliders)
                        {
                            if(CanCollide(collider1, collider2))
                            {
                                CollisionData collisionData = collider1.CheckCollision(collider2);
                                if(collisionData.DidCollide)
                                {
                                    bool shouldResolve=false;
                                    if (collider1.OnCollision.Invoke(collisionData))
                                        shouldResolve = true;

                                    CollisionData collisionData2 = collisionData;
                                    collisionData2.CollisionAngle = -collisionData.CollisionAngle;
                                    collisionData2.MyCollider = collisionData.OtherCollider;
                                    collisionData2.OtherCollider = collisionData.MyCollider;

                                    if (collider2.OnCollision.Invoke(collisionData2))
                                        shouldResolve = true;

                                    if (shouldResolve)
                                        Collider.ResolveCollision(collisionData);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Bodies will automatically add themselves to and remove themselves from the PhysicsManager.
        /// </summary>
        internal List<Body> Bodies
        {
            get
            {
                return bodies;
            }

            set
            {
                bodies = value;
            }
        }
    }

    [Flags]
    public enum CollisionGroup
    {
        None = 0,
        Ground = 1,
        Player = 2
    }
}