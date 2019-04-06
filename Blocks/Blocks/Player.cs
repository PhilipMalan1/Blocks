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
        private float gravity;

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

            turnAnimationSpeed = 2;

            Level = level;
            BlockWidth = blockWidth;
            image = LoadedContent.player;
            facingRight = true;
            onGround = false;
            animationTimer = 0;
            playerState = PlayerState.Standing;

            body = new Body(level.PhysicsMangager, false, 1, gravity, 1);
            body.Pos = SpawnPos;
            body.AddCollider(new CircleCollider(body, CollisionGroup.Player, new Vector2(blockWidth / 2, blockWidth / 2), blockWidth/2, collisionData =>
            {
                if (collisionData.CollisionAngle.Y > Math.Abs(collisionData.CollisionAngle.X))
                {
                    onGround = true;
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

        public override void DataValueName()
        {
            
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
            if (playerState == PlayerState.Turning)
            {
                if (animationTimer >= 2*turnAnimationSpeed)
                {
                    playerState = PlayerState.Standing;
                    facingRight = !facingRight;
                    animationTimer = 0;
                }
                animationTimer++;
            }

            body.Vel = new Vector2(body.Vel.X*horizontalFriction, body.Vel.Y);

            onGround = false;
        }

        public void UpdateInput(GameTime gameTime, KeyboardState key, KeyboardState keyi, MouseState mouse, MouseState mousei)
        {
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
        }
    }

    public enum PlayerState
    {
        Standing,
        Turning
    }
}
