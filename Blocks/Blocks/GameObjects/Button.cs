using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocks
{
    [Serializable]
    class Button : GameObject
    {
        int rotation;

        [NonSerialized]
        Body body;
        [NonSerialized]
        Texture2D image;
        [NonSerialized]
        float height;

        public Button(Level level, float blockWidth, Vector2 spawnPos) : base(level, blockWidth, spawnPos)
        {
            rotation = 0;
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
            return "Object: Button DataValue: rotation";
        }

        public static void DrawIcon(GameTime gameTime, SpriteBatch spriteBatch, Rectangle rect, float blockWidth)
        {
            Texture2D image = LoadedContent.button;
            float height = image.Height * blockWidth / image.Width;
            spriteBatch.Draw(image, new Rectangle(rect.X, (int)(rect.Y+blockWidth-height), (int)blockWidth, (int)height), new Rectangle(0, 0, image.Width, image.Height), Color.White);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera)
        {
            Vector2 origin = new Vector2(image.Width / 2, -image.Width / 2 + image.Height);
            spritebach.Draw(image, new Rectangle((int)(Pos.X - camera.X + BlockWidth / 2), (int)(Pos.Y + BlockWidth - height - camera.Y - height), (int)BlockWidth, (int)height), new Rectangle(0, 0, image.Width, image.Height), Color.White, (float)Math.PI / 2 * rotation, origin, SpriteEffects.None, 0);
        }

        public override void Initialize(Level level, float blockWidth)
        {
            image = LoadedContent.button;
            Level = level;
            BlockWidth = blockWidth;
            height = image.Height * blockWidth / image.Width;

            body = new Body(this, level.PhysicsMangager, true, 1, 0, 0);
            body.Pos = SpawnPos * BlockWidth;
            UpdateRotation();
        }

        private void UpdateRotation()
        {
            if (rotation == 0)
                body.AddCollider(new RectangleCollider(body, CollisionGroup.Ground, new Vector2(0, BlockWidth - height), new Vector2(BlockWidth, height), collisionData => true));
            else if (rotation == 1)
                body.AddCollider(new RectangleCollider(body, CollisionGroup.Ground, new Vector2(0, 0), new Vector2(height, BlockWidth), collisionData => true));
            else if (rotation == 2)
                body.AddCollider(new RectangleCollider(body, CollisionGroup.Ground, new Vector2(0, 0), new Vector2(BlockWidth, height), collisionData => true));
            else if (rotation == 3)
                body.AddCollider(new RectangleCollider(body, CollisionGroup.Ground, new Vector2(BlockWidth-height, 0), new Vector2(height, BlockWidth), collisionData => true));
        }

        public override void NextDataValue()
        {
            rotation++;
            rotation %= 4;
            UpdateRotation();
        }

        public override void PreviousDataValue()
        {
            rotation--;
            rotation %= 4;
            UpdateRotation();
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
