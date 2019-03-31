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

        public override CollisionData CheckCollision(Collider collider)//TODO allow circles to collide with rectangles
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
            else if(collider is RectangleCollider)
            {
                RectangleCollider rectangleCollider = (RectangleCollider)collider;

                Vector2 myPos = Body.Pos + Pos;
                Vector2 otherPos = rectangleCollider.Body.Pos + rectangleCollider.Pos;

                //top left region
                if (myPos.X < otherPos.X && myPos.Y < otherPos.Y)
                {
                    Vector2 r = otherPos - myPos;
                    collisionDepth = radius - r.Length();
                    if(collisionDepth>0)
                    {
                        didCollide = true;
                        r.Normalize();
                        collisionAngle = r;
                    }
                }
                //top right region
                else if (myPos.X > otherPos.X + rectangleCollider.Dimensions.X && myPos.Y < otherPos.Y)
                {
                    Vector2 r = otherPos+new Vector2(rectangleCollider.Dimensions.X, 0) - myPos;
                    collisionDepth = radius - r.Length();
                    if (collisionDepth > 0)
                    {
                        didCollide = true;
                        r.Normalize();
                        collisionAngle = r;
                    }
                }
                //bottom right region
                else if (myPos.X > otherPos.X + rectangleCollider.Dimensions.X && myPos.Y > otherPos.Y + rectangleCollider.Dimensions.Y)
                {
                    Vector2 r = otherPos+rectangleCollider.Dimensions - myPos;
                    collisionDepth = radius - r.Length();
                    if (collisionDepth > 0)
                    {
                        didCollide = true;
                        r.Normalize();
                        collisionAngle = r;
                    }
                }
                //bottom left region
                else if (myPos.X < otherPos.X && myPos.Y > otherPos.Y + rectangleCollider.Dimensions.Y)
                {
                    Vector2 r = otherPos+new Vector2(0, rectangleCollider.Dimensions.Y) - myPos;
                    collisionDepth = radius - r.Length();
                    if (collisionDepth > 0)
                    {
                        didCollide = true;
                        r.Normalize();
                        collisionAngle = r;
                    }
                }
                //if it isn't in any of those regions, it's basically a square
                else
                {
                    Collider rectangularHull = new RectangleCollider(Body, CollisionGroup, new Vector2(Pos.X - Radius, pos.X - Radius), new Vector2(radius * 2, radius * 2), OnCollision);
                    CollisionData collisionData = rectangularHull.CheckCollision(rectangleCollider);
                    didCollide = collisionData.DidCollide;
                    collisionDepth = collisionData.CollisionDepth;
                    collisionAngle = collisionData.CollisionAngle;
                }
            }

            return new CollisionData(didCollide, collisionDepth, collisionAngle, this, collider);
        }
    }
}
