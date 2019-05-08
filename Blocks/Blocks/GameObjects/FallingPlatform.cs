using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    [Serializable]
    class FallingPlatform : GameObject
    {
        [NonSerialized]
        Body body;
        [NonSerialized]
        Texture2D image;
        [NonSerialized]
        bool top, bottom, left, right, topRight, bottomRight, bottomLeft, topLeft;
        [NonSerialized]
        bool shouldLoad;
        float fall;
        public FallingPlatform(Level level, float blockWidth, Vector2 spawnPos) : base(level, blockWidth, spawnPos)
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
            fall = 0;
            BlockWidth = blockWidth*3;
            image = LoadedContent.fP;
            body = new Body(this, level.PhysicsMangager, true, 1, 0, 0);
            body.Pos = SpawnPos * blockWidth;
           body.AddCollider(new RectangleCollider(body, CollisionGroup.Ground, new Vector2(), new Vector2((int)BlockWidth, 27 / 2 + 27 / 4), collisionData =>
            {
                GameObject other = collisionData.OtherCollider.Body.GameObject;

                if (collisionData.OtherCollider.Body.GameObject is Player)
                {
                    if (other.Vel.Y > 0 || body.Gravity != 0)
                    {
                        body.Gravity = BlockWidth * 2;
                        return true;
                    }
                    else
                        return false;
                }

                return true;
            }));

            top = bottom = left = right = topRight = bottomRight = bottomLeft = topLeft = true;

            if (level.CheckForObject((int)SpawnPos.X, -(int)SpawnPos.Y + 1))
                foreach (GameObject gameObject in level.LevelObjects[(int)SpawnPos.X][-(int)SpawnPos.Y + 1])
                {
                    if (gameObject is Ground)
                    {
                        top = false;
                        topLeft = false;
                        topRight = false;
                    }
                }
            if (level.CheckForObject((int)SpawnPos.X, -(int)SpawnPos.Y - 1))
                foreach (GameObject gameObject in level.LevelObjects[(int)SpawnPos.X][-(int)SpawnPos.Y - 1])
                {
                    if (gameObject is Ground)
                    {
                        bottom = false;
                        bottomLeft = false;
                        bottomRight = false;
                    }
                }
            if (level.CheckForObject((int)SpawnPos.X - 1, -(int)SpawnPos.Y))
                foreach (GameObject gameObject in level.LevelObjects[(int)SpawnPos.X - 1][-(int)SpawnPos.Y])
                {
                    if (gameObject is Ground)
                    {
                        left = false;
                        topLeft = false;
                        bottomLeft = false;
                    }
                }
            if (level.CheckForObject((int)SpawnPos.X + 1, -(int)SpawnPos.Y))
                foreach (GameObject gameObject in level.LevelObjects[(int)SpawnPos.X + 1][-(int)SpawnPos.Y])
                {
                    if (gameObject is Ground)
                    {
                        right = false;
                        topRight = false;
                        bottomRight = false;
                    }
                }

            shouldLoad = true;
            if (!top && !bottom && !left && !right && !topRight && !bottomRight && !bottomLeft && !topLeft)
            {
                shouldLoad = false;
                body.Unload();
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

        }

        public static void DrawIcon(GameTime gameTime, SpriteBatch spriteBatch, Rectangle rect)
        {
            rect.Height /= 6;
            rect.Y += 30;
            spriteBatch.Draw(LoadedContent.fP, rect, new Rectangle(0, 0, 324, 27), Color.White, 0, new Vector2(), SpriteEffects.None, (float)DrawLayer.UI / 1000);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera)
        {

            spritebach.Draw(image, new Rectangle((int)(Pos.X - camera.X), (int)(Pos.Y - camera.Y), (int)BlockWidth, 27/2+27/4), new Rectangle(0, 0, 324, 27), Color.White, 0, new Vector2(), SpriteEffects.None, (float)DrawLayer.Ground / 1000);
        }

        public override void Load()
        {
            if (shouldLoad) body.Load();
        }

        public override void Unload()
        {
            body.Unload();

            //respawn if off screen
            Initialize(Level, BlockWidth);
        }
    }
}
