using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    public class Body
    {
        GameObject gameObject;
        bool hasInfiniteMass;
        float mass, gravity, restitution;
        List<Collider> colliders;
        PhysicsManager physicsManager;

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

        public GameObject GameObject
        {
            get
            {
                return gameObject;
            }

            set
            {
                gameObject = value;
            }
        }

        public Body(GameObject gameObject, PhysicsManager physicsManager, bool hasInfiniteMass, float mass, float gravity, float restitution)
        {
            this.gameObject = gameObject;
            this.hasInfiniteMass = hasInfiniteMass;
            this.mass = mass;
            this.gravity = gravity;
            this.restitution = restitution;
            this.physicsManager = physicsManager;
            
            colliders = new List<Collider>();
        }

        public void Load()
        {
            physicsManager.Bodies.Add(this);
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
