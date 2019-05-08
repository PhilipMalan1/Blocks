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
        private Texture2D image;
        [NonSerialized]
        private Body body;
        [NonSerialized]
        private ThrowState throwState;
        [NonSerialized]
        private int landTimer, dropTimer, upThrowTimer;
        [NonSerialized]
        private int landTimerTime;

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

        public override bool LoadIfAlreadyOnScreen
        {
            get
            {
                return true;
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
                    ThrowState1 = ThrowState.Thrown;
                    landTimer = 0;
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
                if (value == ThrowState.Dropped)
                    dropTimer = 0;
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

        public int LandTimer
        {
            get
            {
                return landTimer;
            }

            set
            {
                landTimer = value;
            }
        }

        public override string DataValueName()
        {
            return "Object: Block DataValue: N/A";
        }

        public static void DrawIcon(GameTime gameTime, SpriteBatch spriteBatch, Rectangle rect)
        {
            spriteBatch.Draw(LoadedContent.block, rect, new Rectangle(0, 0, 108, 108), Color.White, 0, new Vector2(), SpriteEffects.None, (float)DrawLayer.UI/1000);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera)
        {
            spritebach.Draw(image, new Rectangle((int)(Pos.X - camera.X), (int)(Pos.Y - camera.Y), (int)BlockWidth, (int)BlockWidth), new Rectangle(0, 0, 108, 108), Color.White, 0, new Vector2(), SpriteEffects.None, (float)DrawLayer.Block/1000);
        }

        public override void Initialize(Level level, float blockWidth)
        {
            landTimerTime = 35;

            Level = level;
            BlockWidth = blockWidth;
            image = LoadedContent.block;
            ThrowState1 = ThrowState.Grabable;

            body = new Body(this, level.PhysicsMangager, false, 1, blockWidth * 25, 1);
            body.Pos = SpawnPos * (int)blockWidth;
            body.Vel = new Vector2();
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
                        if (Pos.Y > other.Pos.Y)
                        {
                            Pos = new Vector2((2 * other.Pos.X + Pos.X) / 3, other.Pos.Y + blockWidth);
                            Vel = new Vector2();
                            ThrowState1 = ThrowState.Jumpable;
                            if (other.Vel.Y < 0) other.Vel = new Vector2(Vel.X, 0);
                        }
                    }
                    //grab
                    else if (!IsHeld && ThrowState1 == ThrowState.Grabable && ((Player)other).HeldItem == null)
                    {
                        ((Player)other).HeldItem = this;
                        IsHeld = true;
                    }
                }

                //if dropped or thrown up, don't collide with player
                if ((ThrowState1 == ThrowState.Dropped || ThrowState1==ThrowState.ThrownUp) && other is Player)
                    shouldntCollide = true;

                //bounce off walls
                if (!IsHeld && other is Ground && Math.Abs(collisionData.CollisionAngle.X)>Math.Abs(collisionData.CollisionAngle.Y))
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

                //if block touches the ground
                if(throwState != ThrowState.Grabable && throwState != ThrowState.Held && other is Ground && collisionData.CollisionAngle.Y > Math.Abs(collisionData.CollisionAngle.X))
                {
                    ThrowState1 = ThrowState.Jumpable;
                    landTimer++;
                }

                //if block hits ceiling
                if((ThrowState1==ThrowState.Thrown || ThrowState1==ThrowState.Jumpable || ThrowState1==ThrowState.ThrownUp) && other is Ground && -collisionData.CollisionAngle.Y > Math.Abs(collisionData.CollisionAngle.X))
                {
                    ThrowState1 = ThrowState.Grabable;
                }

                if (shouldntCollide) return false;
                return ThrowState1!=ThrowState.Held;
            }));

            body.Colliders[0].DidCollide=(collisionData, hasCollided) =>
            {
                GameObject other = collisionData.OtherCollider.Body.GameObject;

                //friction
                if (other is Ground && hasCollided)
                {
                    Vel *= 0.9f;
                }
            };
        }

        public override void NextDataValue()
        {
            
        }

        public override void PreviousDataValue()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            //respawn after falling off screen
            if (Pos.Y > 0)
            {
                Pos = SpawnPos * BlockWidth;
                Vel = new Vector2();
            }

            //set velocity to 0 while held
            if (IsHeld)
                body.Vel = new Vector2();

            //set to not thrown if land timer is hight enough
            if (throwState!=ThrowState.Grabable && throwState != ThrowState.Held)
            {
                if (landTimer >= landTimerTime)
                {
                    ThrowState1 = ThrowState.Grabable;
                    landTimer = 0;
                }
            }
            //drop timer
            if(throwState==ThrowState.Dropped)
            {
                dropTimer++;
                if (dropTimer >= 35)
                {
                    ThrowState1 = ThrowState.Jumpable;
                    dropTimer = 0;
                }
            }
            //up throw timer
            if (throwState == ThrowState.ThrownUp)
            {
                upThrowTimer++;
                if (upThrowTimer >= 60)
                {
                    ThrowState1 = ThrowState.Grabable;
                    upThrowTimer = 0;
                }
            }
        }

        public override void Load()
        {
            body.Load();
        }

        public override void Unload()
        {
            body.Unload();

            //respawn if off screen
            Initialize(Level, BlockWidth);
        }

        public enum ThrowState
        {
            Grabable,
            Held,
            Thrown,
            Jumpable,
            Dropped,
            ThrownUp
        }
    }
}
