using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    class CircleCollider : Collider
    {
        Vector2 pos;
        float radius;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="pos">Position relitive to body Pos.</param>
        /// <param name="radius"></param>
        /// <param name="OnCollision"></param>
        public CircleCollider(Body body, CollisionGroup collisionGroup, Vector2 pos, float radius, Func<CollisionData, bool> onCollision) : base(body, collisionGroup, onCollision)
        {
            this.pos = pos;
            this.radius = radius;
        }

        public Vector2 Pos
        {
            get
            {
                return pos;
            }

            set
            {
                pos = value;
            }
        }

        public float Radius
        {
            get
            {
                return radius;
            }

            set
            {
                radius = value;
            }
        }

        public override CollisionData CheckCollision(Collider collider)
        {
            //these values will be returned if there isn't a collision
            bool didCollide=false;
            float collisionDepth=0;
            Vector2 collisionAngle=new Vector2();

            if (collider is CircleCollider)
            {
                CircleCollider circleCollider = (CircleCollider)collider;

                //check for collision
                Vector2 r = circleCollider.Body.Pos+circleCollider.Pos-(Body.Pos+Pos);
                collisionDepth = (radius + circleCollider.Radius) - r.Length();
                if (collisionDepth > 0)
                {
                    didCollide = true;

                    //collision angle
                    r.Normalize();
                    collisionAngle = r;
                }
            }

            return new CollisionData(didCollide, collisionDepth, collisionAngle, this, collider);
        }
    }
}
