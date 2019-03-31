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
        Func<CollisionData, bool> onCollision;
        CollisionGroup collisionGroup;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="collisionGroup"></param>
        /// <param name="onCollision">If you return true, the collision will be resolved.</param>
        public Collider(Body body, CollisionGroup collisionGroup, Func<CollisionData, bool> onCollision)
        {
            this.body = body;
            this.collisionGroup = collisionGroup;
            this.onCollision = onCollision;
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

        internal CollisionGroup CollisionGroup
        {
            get
            {
                return collisionGroup;
            }

            set
            {
                collisionGroup = value;
            }
        }

        internal Func<CollisionData, bool> OnCollision
        {
            get
            {
                return onCollision;
            }

            set
            {
                onCollision = value;
            }
        }

        abstract public CollisionData CheckCollision(Collider collider);

        /// <summary>
        /// Applies position and velocity changes as a result of the collision.
        /// </summary>
        /// <param name="collider"></param>
        /// <param name="collisionData"></param>
        public static void ResolveCollision(CollisionData collisionData)
        {
            //return if there wan't a collision
            if (!collisionData.DidCollide) return;

            Collider myCollider = collisionData.MyCollider;
            Collider otherCollider = collisionData.OtherCollider;

            float v1xf, v2xf;
            //calculate restitution
            float restitution = Math.Min(myCollider.Body.Restitution, otherCollider.body.Restitution);

            //find the velocity of both objects in the direction of the collision
            float v1xi = Vector2.Dot(myCollider.Body.Vel, collisionData.CollisionAngle);
            float v2xi = Vector2.Dot(otherCollider.Body.Vel, collisionData.CollisionAngle);

            //find time since collision
            float timeSinceCollision;
            if (v1xi - v2xi == 0)
                timeSinceCollision = 0;
            else
                timeSinceCollision = collisionData.CollisionDepth / (v1xi - v2xi);
            if (Math.Abs(timeSinceCollision) > 1f / 60) timeSinceCollision *= 1f / 60 / Math.Abs(timeSinceCollision);

            //It's rewind time! (update positions)
            myCollider.Body.Pos -= myCollider.Body.Vel * timeSinceCollision;
            otherCollider.Body.Pos -= otherCollider.Body.Vel * timeSinceCollision;

            //find the velocity of both objects perpendicular to the collision
            Vector2 v1y = myCollider.body.Vel - v1xi * collisionData.CollisionAngle;
            Vector2 v2y = otherCollider.Body.Vel - v2xi * collisionData.CollisionAngle;

            //calculate the difference of the final velocities (I think?)
            float gamma = restitution * (v1xi - v2xi);

            if (myCollider.body.HasInfiniteMass)
            {
                //if both objects have infinite mass
                if (otherCollider.body.HasInfiniteMass)
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
            else if (otherCollider.body.HasInfiniteMass)
            {
                v1xf = v2xi - gamma;
                v2xf = v2xi;
            }
            //otherwise
            else
            {
                //the big boy equation
                v1xf = ((myCollider.Body.Mass * v1xi + otherCollider.Body.Mass * v2xi) / otherCollider.Body.Mass - gamma) / (1 + myCollider.Body.Mass / otherCollider.Body.Mass);
                v2xf = gamma + v1xf;
            }

            //calculate and apply final velocities
            myCollider.Body.Vel = v1xf * collisionData.CollisionAngle + v1y;
            otherCollider.Body.Vel = v2xf * collisionData.CollisionAngle + v2y;
        }
    }
}
