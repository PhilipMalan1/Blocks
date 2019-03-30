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

        Vector2 pos, vel;//TODO get and set the position from the game object once you start using game objects.

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

        public Vector2 Vel
        {
            get
            {
                return vel;
            }

            set
            {
                vel = value;
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

        public void addCollider(Collider collider)
        {
            collider.Body = this;
            colliders.Add(collider);
        }

        public void Update()
        {
            vel += new Vector2(0, gravity);
            pos += vel;
        }
    }
}
