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
        private int throwTimer;
        [NonSerialized]
        private int throwTimerTime;

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
            spritebach.Draw(image, new Rectangle((int)(Pos.X - camera.X), (int)(Pos.Y - camera.Y), (int)BlockWidth, (int)BlockWidth), new Rectangle(0, 0, 108, 108), Color.White);
        }

        public override void Initialize(Level level, float blockWidth)
        {
            throwTimerTime = 30;

            this.level = level;
            BlockWidth = blockWidth;
            image = LoadedContent.block;
            throwState = ThrowState.NotThrown;

            body = new Body(this, level.PhysicsMangager, false, 1, blockWidth * 25, 1);
            body.Pos = SpawnPos * (int)blockWidth;
            body.Colliders.Add(new RectangleCollider(body, CollisionGroup.Player, new Vector2(), new Vector2(blockWidth, blockWidth), collisionData =>
            {
                if(!IsHeld && Math.Abs(collisionData.CollisionAngle.X)>Math.Abs(collisionData.CollisionAngle.Y))
                {
                    if(collisionData.CollisionAngle.X>0)
                    {
                        Vel = new Vector2(-Math.Abs(Vel.X), Vel.Y);
                    }
                    else if (collisionData.CollisionAngle.X < 0)
                    {
                        Vel = new Vector2(Math.Abs(Vel.X), Vel.Y);
                    }
                    return false;
                }
                if((throwState==ThrowState.Thrown || throwState==ThrowState.ThrowTimerExpired) && /*Downward collision*/)
                {
                    //move to spawn point
                }

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
            if (IsHeld)
                body.Vel = new Vector2();

            if(throwState==ThrowState.Thrown)
            {
                throwTimer++;
                if (throwTimer >= throwTimerTime)
                    throwState = ThrowState.ThrowTimerExpired;
            }
        }

        public enum ThrowState
        {
            NotThrown,
            Held,
            Thrown,
            ThrowTimerExpired
        }
    }
}
