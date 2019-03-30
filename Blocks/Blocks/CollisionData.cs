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
        Vector2 obj1Push, obj2Push, collisionAngle;

        public CollisionData(bool didCollide, Vector2 obj1Push, Vector2 obj2Push, Vector2 collisionAngle)
        {
            this.didCollide = didCollide;
            this.obj1Push = obj1Push;
            this.obj2Push = obj2Push;
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

        public Vector2 Obj1Push
        {
            get
            {
                return obj1Push;
            }

            set
            {
                obj1Push = value;
            }
        }

        public Vector2 Obj2Push
        {
            get
            {
                return obj2Push;
            }

            set
            {
                obj2Push = value;
            }
        }
    }
}
