using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    [Serializable]
    class RisingPlatform : GameObject
    {
        [NonSerialized]
        Body body;
        [NonSerialized]
        Texture2D image;

        public RisingPlatform(Level level, float blockWidth, Vector2 spawnPos) : base(level, blockWidth, spawnPos)
        {

        }

        public override bool LoadIfAlreadyOnScreen
        {
            get
            {
                return true;
            }
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
            return "Object: Falling Platform DataValue: N/A";
        }

        public override void Initialize(Level level, float blockWidth)
        {
            Level = level;
            BlockWidth = blockWidth;
            image = LoadedContent.rP;
            body = new Body(this, level.PhysicsMangager, true, 1, 0, 0);
            body.Pos = SpawnPos * blockWidth;
            body.AddCollider(new RectangleCollider(body, CollisionGroup.Ground, new Vector2(), new Vector2((int)BlockWidth * 3, (int)((float)image.Height / image.Width * 3 * BlockWidth)), collisionData =>
            {
                GameObject other = collisionData.OtherCollider.Body.GameObject;

                if (other is Player && collisionData.CollisionAngle == new Vector2(0, -1))
                {
                    if (other.Vel.Y > 0 || body.Gravity != 0)
                    {
                        body.Gravity = -BlockWidth * 6;
                        return true;
                    }
                    else
                        return false;
                }

                return true;
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
            if (Vel.Y < -BlockWidth*100)
                Vel = new Vector2(Vel.X, -BlockWidth*100);
        }

        public static void DrawIcon(GameTime gameTime, SpriteBatch spriteBatch, Rectangle rect)
        {
            rect.Height /= 6;
            rect.Y += 30;
            spriteBatch.Draw(LoadedContent.rP, rect, new Rectangle(0, 0, 324, 27), Color.White, 0, new Vector2(), SpriteEffects.None, (float)DrawLayer.UI / 1000);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera)
        {

            spritebach.Draw(image, new Rectangle((int)(Pos.X - camera.X), (int)(Pos.Y - camera.Y), (int)BlockWidth * 3, (int)((float)image.Height / image.Width * 3 * BlockWidth)), new Rectangle(0, 0, image.Width, image.Height), Color.White, 0, new Vector2(), SpriteEffects.None, (float)DrawLayer.RisingPlatform / 1000);
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
    }
}
