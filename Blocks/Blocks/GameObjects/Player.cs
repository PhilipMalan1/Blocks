using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blocks
{
    [Serializable]
    public class Player : GameObject, IInput, ICameraFocus
    {
        [NonSerialized]
        private Body body;
        [NonSerialized]
        private Texture2D image;
        [NonSerialized]
        private bool facingRight;
        [NonSerialized]
        private bool onGround;
        [NonSerialized]
        private int animationTimer;
        [NonSerialized]
        private PlayerState playerState;
        [NonSerialized]
        private float walkAcc;
        [NonSerialized]
        private float horizontalFriction;
        [NonSerialized]
        private int turnAnimationSpeed;
        [NonSerialized]
        private float jumpSpeed;
        [NonSerialized]
        private GameObject heldItem;

        public Player(Level level, float blockWidth, Vector2 spawnPos) : base(level, blockWidth, spawnPos)
        {

        }

        public override void Initialize(Level level, float blockWidth)
        {
            float maxSpeed = blockWidth*15;
            walkAcc = maxSpeed/5;
            float jumpHeight = blockWidth * 4;
            float gravity = blockWidth * 50;
            horizontalFriction = 1 - walkAcc / maxSpeed;
            jumpSpeed = (float)Math.Sqrt(2 * gravity * jumpHeight);

            turnAnimationSpeed = 4;

            Level = level;
            BlockWidth = blockWidth;
            image = LoadedContent.player;
            facingRight = true;
            onGround = false;
            animationTimer = 0;
            playerState = PlayerState.Standing;

            body = new Body(this, level.PhysicsMangager, false, 1, gravity, 1);
            body.Pos = SpawnPos*blockWidth;
            body.AddCollider(new CircleCollider(body, CollisionGroup.Player, new Vector2(blockWidth / 2, blockWidth / 2), blockWidth/2, collisionData =>
            {
                GameObject other = collisionData.OtherCollider.Body.GameObject;

                //regain jump
                if (collisionData.CollisionAngle.Y > Math.Abs(collisionData.CollisionAngle.X))
                {
                    onGround = true;
                }

                //block stuff
                if(other is Block && !((Block)other).IsHeld)
                {
                    //block jump
                    if (((Block)other).ThrowState1==Block.ThrowState.Thrown)
                    {
                        if (((Block)other).HasTeleported)
                            return true;
                        if (other.Pos.Y>Pos.Y)
                        {
                            other.Pos = new Vector2((Pos.X+other.Pos.X)/2, Pos.Y+blockWidth);
                            ((Block)other).Vel = new Vector2();
                            ((Block)other).HasTeleported = true;
                            onGround = true;
                            if (Vel.Y < 0) Vel = new Vector2(Vel.X, 0);
                            return false;
                        }
                    }
                    else
                    {
                        heldItem = other;
                        ((Block)other).IsHeld = true;
                    }
                }

                return true;
            }));
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
            return "Object: Player DataValue: N/A";
        }

        public static void DrawIcon(GameTime gameTime, SpriteBatch spriteBatch, Rectangle rect)
        {
            spriteBatch.Draw(LoadedContent.player, rect, new Rectangle(0, 0, 108, 108), Color.White);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera)
        {
            if (playerState == PlayerState.Standing)
            {
                if (facingRight)
                    spritebach.Draw(image, new Rectangle((int)(Pos.X - camera.X), (int)(Pos.Y - camera.Y), (int)BlockWidth, (int)BlockWidth), new Rectangle(0, 0, 108, 108), Color.White);
                else
                    spritebach.Draw(image, new Rectangle((int)(Pos.X - camera.X), (int)(Pos.Y - camera.Y), (int)BlockWidth, (int)BlockWidth), new Rectangle(0, 0, 108, 108), Color.White, 0, new Vector2(),
                        SpriteEffects.FlipHorizontally, 0);
            }

            else if (playerState == PlayerState.Turning)
            {
                int animationSpeed = turnAnimationSpeed;

                bool isFlipped = facingRight;
                if (animationTimer > 2*animationSpeed-1) isFlipped = !isFlipped;
                if (animationTimer % (2*animationSpeed) <= animationSpeed)
                {
                    if(isFlipped)
                        spritebach.Draw(image, new Rectangle((int)(Pos.X - camera.X), (int)(Pos.Y - camera.Y), (int)BlockWidth, (int)BlockWidth), new Rectangle(108, 0, 108, 108), Color.White);
                    else
                        spritebach.Draw(image, new Rectangle((int)(Pos.X - camera.X), (int)(Pos.Y - camera.Y), (int)BlockWidth, (int)BlockWidth), new Rectangle(108, 0, 108, 108), Color.White, 0, new Vector2(), 
                            SpriteEffects.FlipHorizontally, 0);
                } 
                else
                {
                    if (isFlipped)
                        spritebach.Draw(image, new Rectangle((int)(Pos.X - camera.X), (int)(Pos.Y - camera.Y), (int)BlockWidth, (int)BlockWidth), new Rectangle(0, 108, 108, 108), Color.White);
                    else
                        spritebach.Draw(image, new Rectangle((int)(Pos.X - camera.X), (int)(Pos.Y - camera.Y), (int)BlockWidth, (int)BlockWidth), new Rectangle(0, 108, 108, 108), Color.White,
                            0, new Vector2(), SpriteEffects.FlipHorizontally, 0);
                }
            }
        }

        public override void NextDataValue()
        {
            
        }

        public override void PreviousDataValue()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (Pos.X < 0)
                Pos=new Vector2(0, Pos.Y);
            if (Pos.Y > 0)
                Die();

            if (playerState == PlayerState.Turning)
            {
                if (animationTimer >= 2*turnAnimationSpeed)
                {
                    playerState = PlayerState.Standing;
                    facingRight = !facingRight;
                    animationTimer = 0;
                }
                animationTimer++;

                //held item turn animation
                if(heldItem!=null)
                {
                    Vector2 itemDest;
                    if (facingRight)
                        itemDest = new Vector2(Pos.X - BlockWidth, Pos.Y);
                    else
                        itemDest = new Vector2(Pos.X + BlockWidth, Pos.Y);

                    heldItem.Pos += (itemDest - heldItem.Pos) / 5;
                }
            }

            body.Vel = new Vector2(body.Vel.X*horizontalFriction, body.Vel.Y);

            //held item
            if (playerState!=PlayerState.Turning && heldItem!=null)
            {
                if (facingRight)
                    heldItem.Pos = new Vector2(Pos.X + BlockWidth, Pos.Y);
                
                else
                    heldItem.Pos = new Vector2(Pos.X - ((IHoldable)heldItem).Width, Pos.Y);
            }
            if(heldItem!=null)
                heldItem.Pos += Vel/60;

            onGround = false;
        }

        public void UpdateInput(GameTime gameTime, KeyboardState key, KeyboardState keyi, MouseState mouse, MouseState mousei)
        {
            //move
            if (key.IsKeyDown(Keys.D))
            {
                body.Vel += new Vector2(walkAcc, 0);
                if (!facingRight)
                    playerState = PlayerState.Turning;
            }
            if (key.IsKeyDown(Keys.A))
            {
                body.Vel += new Vector2(-walkAcc, 0);
                if (facingRight)
                    playerState = PlayerState.Turning;
            }
            if (key.IsKeyDown(Keys.W) && keyi.IsKeyUp(Keys.W) && onGround)
            {
                body.Vel = new Vector2(0, -jumpSpeed);
            }

            //throw
            if(heldItem!=null)
            {
                if(key.IsKeyDown(Keys.Up))
                {
                    heldItem.Vel = new Vector2(0, -BlockWidth * 20);

                    ((IHoldable)heldItem).IsHeld = false;
                    heldItem = null;
                }
                else if (key.IsKeyDown(Keys.Down))
                {
                    heldItem.Vel = new Vector2();

                    ((IHoldable)heldItem).IsHeld = false;
                    heldItem = null;
                }
                else if (key.IsKeyDown(Keys.Left) || key.IsKeyDown(Keys.Right))
                {
                    if (facingRight)
                    {
                        heldItem.Vel = new Vector2(BlockWidth * 20, -BlockWidth*5);

                        ((IHoldable)heldItem).IsHeld = false;
                        heldItem = null;
                    }
                    if (!facingRight)
                    {
                        heldItem.Vel = new Vector2(-BlockWidth * 20, -BlockWidth*5);

                        ((IHoldable)heldItem).IsHeld = false;
                        heldItem = null;
                    }
                }
            }
        }

        public void Die()
        {
            Level.Initialize(BlockWidth);
        }
    }

    public enum PlayerState
    {
        Standing,
        Turning
    }
}
