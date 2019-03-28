using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    class Body
    {
        bool hasInfiniteMass;
        float mass, gravity, restitution;
        List<Collider> colliders;

        Vector2 pos, vel;

        public bool HasInfiniteMass
        {
            get
            {
                return hasInfiniteMass;
            }

            set
            {
                hasInfiniteMass = value;
            }
        }

        public float Mass
        {
            get
            {
                return mass;
            }

            set
            {
                mass = value;
            }
        }

        public float Gravity
        {
            get
            {
                return gravity;
            }

            set
            {
                gravity = value;
            }
        }

        public float Restitution
        {
            get
            {
                return restitution;
            }

            set
            {
                restitution = value;
            }
        }

        internal List<Collider> Colliders
        {
            get
            {
                return colliders;
            }

            set
            {
                colliders = value;
            }
        }

        public Body(bool hasInfiniteMass, float mass, float gravity, float restitution, List<Collider> colliders)
        {
            this.hasInfiniteMass = hasInfiniteMass;
            this.mass = mass;
            this.gravity = gravity;
            this.restitution = restitution;
            this.colliders = colliders;
        }

        public void Update()
        {
            vel += new Vector2(0, gravity);
            pos += vel;
        }
    }
}
