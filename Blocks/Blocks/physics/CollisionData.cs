using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    struct CollisionData
    {
        Collider myCollider, otherCollider;
        bool didCollide;
        float collisionDepth;
        Vector2 collisionAngle;

        public CollisionData(bool didCollide, float collisionDepth, Vector2 collisionAngle, Collider myCollider, Collider otherCollider)
        {
            this.didCollide = didCollide;
            this.collisionDepth = collisionDepth;
            this.collisionAngle = collisionAngle;
            this.myCollider = myCollider;
            this.otherCollider = otherCollider;
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

        internal Collider MyCollider
        {
            get
            {
                return myCollider;
            }

            set
            {
                myCollider = value;
            }
        }

        internal Collider OtherCollider
        {
            get
            {
                return otherCollider;
            }

            set
            {
                otherCollider = value;
            }
        }
    }
}
