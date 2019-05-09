using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocks
{
    class Arrow2 : GameObject
    {
        bool isFlipped;

        [NonSerialized]
        Body body;
        [NonSerialized]
        Texture2D image;

        public Arrow2(Level level, float blockWidth, Vector2 spawnPos) : base(level, blockWidth, spawnPos)
        {
            isFlipped = false;
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
            return "Object: Arrow2 (Block jump indicator) Data Value: Horizontal Flip";
        }

        public static void DrawIcon(GameTime gameTime, SpriteBatch spriteBatch, Rectangle rect)
        {
            Texture2D image = LoadedContent.arrow2;
            spriteBatch.Draw(LoadedContent.arrow2, rect, new Rectangle(0, 0, image.Width, image.Height), Color.White, 0, new Vector2(), SpriteEffects.None, (float)DrawLayer.UI / 1000);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera)
        {
            spritebach.Draw(image, new Rectangle((int)(Pos.X - camera.X), (int)(Pos.Y - camera.Y), (int)BlockWidth, (int)BlockWidth), new Rectangle(0, 0, 108, 108), Color.White, 0, new Vector2(), isFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, (float)DrawLayer.Arrow2 / 1000);
        }

        public override void Initialize(Level level, float blockWidth)
        {
            Level = level;
            BlockWidth = blockWidth;
            image = LoadedContent.arrow2;
            body = new Body(this, level.PhysicsMangager, true, 1, 0, 0);
            body.Pos = SpawnPos * blockWidth;
            body.AddCollider(new RectangleCollider(body, CollisionGroup.Ground, new Vector2(), new Vector2(blockWidth, blockWidth), collisionData => false));
        }

        public override void Load()
        {
            body.Load();
        }

        public override void NextDataValue()
        {
            isFlipped = !isFlipped;
        }

        public override void PreviousDataValue()
        {
            isFlipped = !isFlipped;
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
