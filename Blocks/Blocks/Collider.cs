using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    abstract class Collider
    {
        Body body;
        Action OnCollision;

        public Collider(Body body, Action OnCollision)
        {
            this.body = body;
            this.OnCollision = OnCollision;
        }

        internal Body Body
        {
            get
            {
                return body;
            }

            set
            {
                body = value;
            }
        }

        abstract public CollisionData CheckCollision(Collider collider);

        /// <summary>
        /// Applies position and velocity changes as a result of the collision.
        /// </summary>
        /// <param name="collider"></param>
        /// <param name="collisionData"></param>
        public void ResolveCollision(Collider collider, CollisionData collisionData)
        {
            float v1xf, v2xf;
            //calculate restitution
            float restitution = Math.Min(body.Restitution, collider.body.Restitution);

            //find the velocity of both objects in the direction of the collision
            float v1xi = Vector2.Dot(body.Vel, collisionData.CollisionAngle);
            float v2xi = Vector2.Dot(collider.Body.Vel, collisionData.CollisionAngle);

            //find time since collision
            float timeSinceCollision;
            if (v1xi + v2xi == 0)
                timeSinceCollision = 0;
            else
                timeSinceCollision = collisionData.CollisionDepth / (v1xi + v2xi);

            //It's rewind time! (update positions)
            Body.Pos -= Body.Vel * timeSinceCollision;
            collider.Body.Pos -= collider.Body.Vel * timeSinceCollision;

            //find the velocity of both objects perpendicular to the collision
            Vector2 v1y = body.Vel - v1xi * collisionData.CollisionAngle;
            Vector2 v2y = collider.Body.Vel - v2xi * collisionData.CollisionAngle;

            //calculate the difference of the final velocities (I think?)
            float gamma = restitution * (v1xi - v2xi);

            if (body.HasInfiniteMass)
            {
                //if both objects have infinite mass
                if (collider.body.HasInfiniteMass)
                {
                    v1xf = v1xi;
                    v2xf = v2xi;
                }
                //if this object has infinite mass
                else
                {
                    v1xf = v1xi;
                    v2xf = gamma + v1xi;
                }
            }
            //if the other object has infinite mass
            else if (collider.body.HasInfiniteMass)
            {
                v1xf = v2xi - gamma;
                v2xf = v2xi;
            }
            //otherwise
            else
            {
                //the big boy equation
                v1xf = ((Body.Mass * v1xi + collider.Body.Mass * v2xi) / collider.Body.Mass - gamma) / (1 + Body.Mass / collider.Body.Mass);
                v2xf = gamma + v1xf;
            }

            //calculate and apply final velocities
            Body.Vel = v1xf * collisionData.CollisionAngle + v1y;
            collider.Body.Vel = v2xf * collisionData.CollisionAngle + v1y;
        }
    }
}
