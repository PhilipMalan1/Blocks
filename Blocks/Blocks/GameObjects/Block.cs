using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocks
{
    [Serializable]
    public class Block : GameObject, IHoldable
    {
        [NonSerialized]
        private Level level;
        [NonSerialized]
        private Texture2D image;
        [NonSerialized]
        private Body body;
        [NonSerialized]
        private ThrowState throwState;
        [NonSerialized]
        private int throwTimer, dropTimer;
        [NonSerialized]
        private int throwTimerTime;
        [NonSerialized]
        private bool hasTeleported;

        public Block(Level level, float blockWidth, Vector2 spawnPos) : base(level, blockWidth, spawnPos)
        {
        }

        public override Vector2 Pos
        {
            get
            {
                return body.Pos;
            }

            set
            {
                body.Pos = value;
            }
        }

        public bool IsHeld
        {
            get
            {
                return throwState == ThrowState.Held;
            }

            set
            {
                if (value)
                    throwState = ThrowState.Held;
                else
                {
                    throwState = ThrowState.Thrown;
                    throwTimer = 0;
                    hasTeleported = false;
                }
            }
        }

        public float Width
        {
            get
            {
                return BlockWidth;
            }
        }

        public ThrowState ThrowState1
        {
            get
            {
                return throwState;
            }

            set
            {
                throwState = value;
            }
        }

        public override Vector2 Vel
        {
            get
            {
                return body.Vel;
            }

            set
            {
                body.Vel = value;
            }
        }

        public bool HasTeleported
        {
            get
            {
                return hasTeleported;
            }

            set
            {
                hasTeleported = value;
            }
        }

        public int ThrowTimer
        {
            get
            {
                return throwTimer;
            }

            set
            {
                throwTimer = value;
            }
        }

        public override string DataValueName()
        {
            return "Object: Block DataValue: N/A";
        }

        public static void DrawIcon(GameTime gameTime, SpriteBatch spriteBatch, Rectangle rect)
        {
            spriteBatch.Draw(LoadedContent.block, rect, new Rectangle(0, 0, 108, 108), Color.White);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera)
        {
            spritebach.Draw(image, new Rectangle((int)(Pos.X - camera.X), (int)(Pos.Y - camera.Y), (int)BlockWidth, (int)BlockWidth), new Rectangle(0, 0, 108, 108), Color.White, 0, new Vector2(), SpriteEffects.None, (float)DrawLayer.Block/1000);
        }

        public override void Initialize(Level level, float blockWidth)
        {
            throwTimerTime = 15;

            this.level = level;
            BlockWidth = blockWidth;
            image = LoadedContent.block;
            throwState = ThrowState.NotThrown;

            body = new Body(this, level.PhysicsMangager, false, 1, blockWidth * 25, 1);
            body.Pos = SpawnPos * (int)blockWidth;
            body.Colliders.Add(new RectangleCollider(body, CollisionGroup.Player, new Vector2(), new Vector2(blockWidth, blockWidth), collisionData =>
            {
                GameObject other = collisionData.OtherCollider.Body.GameObject;
                bool shouldntCollide = false;

                //player
                if (other is Player && !IsHeld)
                {
                    //block jump
                    if (ThrowState1 == ThrowState.Thrown)
                    {
                        if (HasTeleported)
                            return true;
                        if (Pos.Y > other.Pos.Y)
                        {
                            Pos = new Vector2((2 * other.Pos.X + Pos.X) / 3, other.Pos.Y + blockWidth);
                            Vel = new Vector2();
                            HasTeleported = true;
                            if (Vel.Y < 0) Vel = new Vector2(Vel.X, 0);
                            return false;
                        }
                    }
                    else if (!IsHeld && (ThrowState1 == ThrowState.NotThrown || ThrowState1 == ThrowState.ThrowTimerExpired) && ((Player)other).HeldItem == null)
                    {
                        ((Player)other).HeldItem = this;
                        IsHeld = true;
                    }
                }

                //bounce off walls
                if (!IsHeld && Math.Abs(collisionData.CollisionAngle.X)>Math.Abs(collisionData.CollisionAngle.Y))
                {
                    if(collisionData.CollisionAngle.X>0)
                    {
                        Vel = new Vector2(-Math.Abs(Vel.X), Vel.Y);
                    }
                    else if (collisionData.CollisionAngle.X < 0)
                    {
                        Vel = new Vector2(Math.Abs(Vel.X), Vel.Y);
                    }
                    shouldntCollide = true;
                }

                //increment throw timer
                if(throwState==ThrowState.Thrown && other is Ground)
                {
                    throwTimer++;
                }

                //friction
                if(other is Ground)
                {
                    Vel *= 0.98f;
                }

                if (shouldntCollide) return false;
                return !IsHeld;
            }));
        }

        public override void NextDataValue()
        {
            
        }

        public override void PreviousDataValue()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            //respawn if falling off screen
            if (Pos.Y > 0)
                Initialize(level, BlockWidth);

            if (IsHeld)
                body.Vel = new Vector2();

            if(throwState==ThrowState.Thrown)
            {
                if (throwTimer >= throwTimerTime)
                    throwState = ThrowState.ThrowTimerExpired;
            }
            else if(throwState==ThrowState.Dropped)
            {
                throwTimer++;
                if (throwTimer >= 60)
                    throwState = ThrowState.ThrowTimerExpired;
            }
        }

        public enum ThrowState
        {
            NotThrown,
            Held,
            Thrown,
            Dropped,
            ThrowTimerExpired
        }
    }
}
