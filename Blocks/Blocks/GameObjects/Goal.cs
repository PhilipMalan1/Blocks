using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocks
{
    [Serializable]
    public class Goal : GameObject
    {
        [NonSerialized]
        private Texture2D image;
        [NonSerialized]
        private Body body;
        [NonSerialized]
        private float height;

        public Goal(Level level, float blockWidth, Vector2 spawnPos) : base(level, blockWidth, spawnPos)
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
            return "Object: Goal DataValue: N/A";
        }

        public static void DrawIcon(GameTime gameTime, SpriteBatch spriteBatch, Rectangle rect)
        {
            spriteBatch.Draw(LoadedContent.goal, rect, new Rectangle(0, 0, 108, 108), Color.White, 0, new Vector2(), SpriteEffects.None, (float)DrawLayer.UI / 1000);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera)
        {
            spritebach.Draw(image, new Rectangle((int)(Pos.X - camera.X), (int)(Pos.Y - camera.Y + BlockWidth - height), (int)BlockWidth, (int)height), new Rectangle(0, 0, image.Width, image.Height), Color.White, 0, new Vector2(), SpriteEffects.None, (float)DrawLayer.Goal/1000);
        }

        public override void Initialize(Level level, float blockWidth)
        {
            Level = level;
            BlockWidth = blockWidth;
            image = LoadedContent.goal;
            height = (float)image.Height / image.Width * BlockWidth;

            body = new Body(this, level.PhysicsMangager, true, 1, 0, 0);
            body.Pos = SpawnPos * blockWidth;
            body.AddCollider(new RectangleCollider(body, CollisionGroup.Ground, new Vector2(0, blockWidth - height), new Vector2(BlockWidth, height), collisionData =>
            {
                if (collisionData.OtherCollider.Body.GameObject is Player)
                    level.Reset();
                return false;
            }));
        }

        public override void Load()
        {
            body.Load();
        }

        public override void NextDataValue()
        {
            
        }

        public override void PreviousDataValue()
        {
            
        }

        public override void Unload()
        {
            body.Unload();
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
