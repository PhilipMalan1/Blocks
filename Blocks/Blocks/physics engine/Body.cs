using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    public class Body
    {
        bool hasInfiniteMass;
        float mass, gravity, restitution;
        List<Collider> colliders;
        PhysicsManager physicsManager;

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

        internal PhysicsManager PhysicsManager
        {
            get
            {
                return physicsManager;
            }

            set
            {
                physicsManager = value;
            }
        }

        public Body(PhysicsManager physicsManager, bool hasInfiniteMass, float mass, float gravity, float restitution)
        {
            this.hasInfiniteMass = hasInfiniteMass;
            this.mass = mass;
            this.gravity = gravity;
            this.restitution = restitution;

            physicsManager.Bodies.Add(this);
            colliders = new List<Collider>();
        }

        public void AddCollider(Collider collider)
        {
            collider.Body = this;
            colliders.Add(collider);
        }

        public void Update(float dt)
        {
            Vector2 netForce = new Vector2(0, gravity);
            vel += netForce * dt;
            pos += vel * dt;
        }

        /// <summary>
        /// Removes Body from Physics manager.
        /// </summary>
        public void Unload()
        {
            physicsManager.Bodies.Remove(this);
        }
    }
}
