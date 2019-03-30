using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    class CollisionData
    {
        bool didCollide;
        float collisionDepth;
        Vector2 collisionAngle;

        public CollisionData(bool didCollide, float collisionDepth, Vector2 collisionAngle)
        {
            this.didCollide = didCollide;
            this.collisionDepth = collisionDepth;
            this.collisionAngle = collisionAngle;
        }

        public Vector2 CollisionAngle
        {
            get
            {
                return collisionAngle;
            }

            set
            {
                collisionAngle = value;
            }
        }

        public bool DidCollide
        {
            get
            {
                return didCollide;
            }

            set
            {
                didCollide = value;
            }
        }

        public float CollisionDepth
        {
            get
            {
                return collisionDepth;
            }

            set
            {
                collisionDepth = value;
            }
        }
    }
}
