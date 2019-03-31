using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    class RectangleCollider : Collider
    {
        Vector2 pos, dimensions;

        public RectangleCollider(Body body, CollisionGroup collisionGroup, Vector2 pos, Vector2 dimensions, Func<CollisionData, bool> OnCollision) : base(body, collisionGroup, OnCollision)
        {
            this.dimensions = dimensions;
            this.pos = pos;
        }

        public Vector2 Dimensions
        {
            get
            {
                return dimensions;
            }

            set
            {
                dimensions = value;
            }
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

        public override CollisionData CheckCollision(Collider collider)
        {
            //these values will be returned if there isn't a collision
            bool didCollide = false;
            float collisionDepth = 0;
            Vector2 collisionAngle = new Vector2();

            if(collider is RectangleCollider)
            {
                RectangleCollider rectangleCollider = (RectangleCollider)collider;
                Vector2 myPos = Body.Pos + Pos;
                Vector2 otherPos = rectangleCollider.Body.Pos + rectangleCollider.Pos;

                //check for collision
                if(myPos.Y<otherPos.Y+rectangleCollider.Dimensions.Y && myPos.Y+Dimensions.Y>otherPos.Y && myPos.X<otherPos.X+rectangleCollider.Dimensions.X && myPos.X+Dimensions.X>otherPos.X)
                {
                    didCollide = true;

                    //check if it's a vertical collision
                    Vector2 r = otherPos - myPos;
                    if(Math.Abs(r.Y)>=Math.Abs(r.X))
                    {
                        //collision depth
                        collisionDepth = Dimensions.Y / 2 + rectangleCollider.Dimensions.Y / 2 - Math.Abs(myPos.Y - otherPos.Y);
                        //collision angle
                        if (myPos.Y < otherPos.Y)
                        {
                            collisionAngle = new Vector2(0, 1);
                        }
                        else
                        {
                            collisionAngle = new Vector2(0, -1);
                        }
                    }
                    //if it's a horizontal collision
                    else
                    {
                        //collision depth
                        collisionDepth = Dimensions.X / 2 + rectangleCollider.Dimensions.X / 2 - Math.Abs(myPos.X - otherPos.X);
                        //collision angle
                        if (myPos.X < otherPos.X)
                        {
                            collisionAngle = new Vector2(1, 0);
                        }
                        else
                        {
                            collisionAngle = new Vector2(-1, 0);
                        }
                    }
                }
            }
            return new CollisionData(didCollide, collisionDepth, collisionAngle, this, collider);
        }
    }
}
