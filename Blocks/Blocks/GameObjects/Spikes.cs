using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocks
{
    [Serializable]
    class Spikes : GameObject
    {
        private int rotation;
        [NonSerialized]
        Body body;
        [NonSerialized]
        Texture2D image;
       [NonSerialized]
        bool top, bottom, left, right, topRight, bottomRight, bottomLeft, topLeft;
        [NonSerialized]
        private float height;
        [NonSerialized]
        private int frame;

        public Spikes(Level level, float blockWidth, Vector2 spawnPos) : base(level, blockWidth, spawnPos)
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
            return "Object:  Spikes";
        }

        public override void Initialize(Level level, float blockWidth)
        {
            Level = level;
            BlockWidth = blockWidth;
            image = LoadedContent.spikes;
            body = new Body(this, level.PhysicsMangager, true, 1, 0, 0);
            body.Pos = SpawnPos * blockWidth;
            height = ((LoadedContent.spikes.Width/LoadedContent.spikes.Width)*BlockWidth)/2;
            UpdateRotation();
            
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
            if (rotation < 0)
                rotation += 4;
            UpdateRotation();
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public static void DrawIcon(GameTime gameTime, SpriteBatch spriteBatch, Rectangle rect)
        {

            spriteBatch.Draw(LoadedContent.spikes, rect, new Rectangle(0, 0, LoadedContent.spikes.Width, LoadedContent.spikes.Height), Color.White, 0, new Vector2(), SpriteEffects.None, (float)DrawLayer.UI / 1000);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera)
        {
            Vector2 origin = new Vector2(image.Width / 2, -image.Width / 2 + image.Height);
            spritebach.Draw(image, new Rectangle((int)((Pos.X - camera.X)+(BlockWidth/2)), (int)(Pos.Y - camera.Y + (BlockWidth/2)), (int)BlockWidth, (int)BlockWidth/2), new Rectangle(0, 0, image.Width, image.Height), Color.White, (float)Math.PI / 2 * rotation, origin, SpriteEffects.None, (float)DrawLayer.Spikes / 1000);
        }

        public override void Load()
        {
            body.Load();
        }

        public override void Unload()
        {
            body.Unload();
        }

        private void UpdateRotation()
        {
            Func<CollisionData, bool> onCollision = collisionData =>
            {
                if (collisionData.OtherCollider.Body.GameObject is Player)
                    Level.Reset();
                return false;
            };

            if (rotation == 0)
                body.AddCollider(new RectangleCollider(body, CollisionGroup.Ground, new Vector2(0, BlockWidth - height), new Vector2(BlockWidth, height), onCollision));
            else if (rotation == 1)
                body.AddCollider(new RectangleCollider(body, CollisionGroup.Ground, new Vector2(0, 0), new Vector2(height, BlockWidth), onCollision));
            else if (rotation == 2)
                body.AddCollider(new RectangleCollider(body, CollisionGroup.Ground, new Vector2(0, 0), new Vector2(BlockWidth, height), onCollision));
            else if (rotation == 3)
                body.AddCollider(new RectangleCollider(body, CollisionGroup.Ground, new Vector2(BlockWidth - height, 0), new Vector2(height, BlockWidth), onCollision));
        }
        



    }
}
