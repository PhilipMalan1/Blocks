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
        Body body;
        [NonSerialized]
        Texture2D image;
        [NonSerialized]
        bool facingRight;

        public Player(Level level, float blockWidth, Vector2 spawnPos) : base(level, blockWidth, spawnPos)
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

        public override void DataValueName()
        {
            
        }

        public static void DrawIcon(GameTime gameTime, SpriteBatch spriteBatch, Rectangle rect)
        {
            spriteBatch.Draw(LoadedContent.player, rect, new Rectangle(0, 0, 108, 108), Color.White);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera)
        {
            if(facingRight)
                spritebach.Draw(image, new Rectangle((int)(Pos.X - camera.X), (int)(Pos.Y - camera.Y), (int)BlockWidth, (int)BlockWidth), new Rectangle(0, 0, 108, 108), Color.White);
            else
                spritebach.Draw(image, new Rectangle((int)(Pos.X - camera.X), (int)(Pos.Y - camera.Y), (int)BlockWidth, (int)BlockWidth), new Rectangle(0, 0, 108, 108), Color.White, 0, new Vector2(), SpriteEffects.FlipHorizontally, 0);
        }

        public override void Initialize(Level level, float blockWidth)
        {
            Level = level;
            BlockWidth = blockWidth;
            image = LoadedContent.player;
            body = new Body(level.PhysicsMangager, false, 1, 1000, 1);
            body.Pos = SpawnPos;
            body.AddCollider(new CircleCollider(body, CollisionGroup.Player, new Vector2(blockWidth/2, blockWidth/2), blockWidth, collisionData => true));
            facingRight = true;
        }

        public override void NextDataValue()
        {
            
        }

        public override void PreviousDataValue()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public void UpdateInput(GameTime gameTime, KeyboardState key, KeyboardState keyi, MouseState mouse, MouseState mousei)
        {
            
        }
    }
}
