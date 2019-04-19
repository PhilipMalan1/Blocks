using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocks
{
    [Serializable]
    class Ground : GameObject
    {
        [NonSerialized]
        Body body;
        [NonSerialized]
        Texture2D image;
        [NonSerialized]
        bool top, bottom, left, right, topRight, bottomRight, bottomLeft, topLeft;

        public Ground(Level level, float blockWidth, Vector2 spawnPos) : base(level, blockWidth, spawnPos)
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
            return "Object: Ground DataValue: N/A";
        }

        public override void Initialize(Level level, float blockWidth)
        {
            Level = level;
            BlockWidth = blockWidth;
            image = LoadedContent.ground;
            body = new Body(this, level.PhysicsMangager, true, 1, 0, 0);
            body.Pos = SpawnPos*blockWidth;
            body.AddCollider(new RectangleCollider(body, CollisionGroup.Ground, new Vector2(), new Vector2(blockWidth, blockWidth), collisionData =>
            {
                if (collisionData.CollisionAngle.Y == -1)
                    if(!top)
                        return false;
                else if (collisionData.CollisionAngle.Y == 1)
                    if (!bottom)
                        return false;
                else if (collisionData.CollisionAngle.X == -1)
                    if (!left)
                        return false;
                else if (collisionData.CollisionAngle.X == 1)
                    if (!right)
                        return false;
                else if(collisionData.CollisionAngle.X<0)
                {
                    if (collisionData.CollisionAngle.Y < 0 && !topLeft)
                        return false;
                    else if (collisionData.CollisionAngle.Y > 0 && !bottomLeft)
                        return false;
                }
                else if(collisionData.CollisionAngle.X>0)
                {
                    if (collisionData.CollisionAngle.Y < 0 && !topRight)
                        return false;
                    else if (collisionData.CollisionAngle.Y > 0 && !bottomRight)
                        return false;
                }
                return true;
            }));

            top = bottom = left = right = topRight = bottomRight = bottomLeft = topLeft = true;

            if (level.CheckForObject((int)SpawnPos.X, -(int)SpawnPos.Y + 1))
                foreach(GameObject gameObject in level.LevelObjects[(int)SpawnPos.X][-(int)SpawnPos.Y + 1])
                {
                    if(gameObject is Ground)
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
            if (level.CheckForObject((int)SpawnPos.X-1, -(int)SpawnPos.Y))
                foreach (GameObject gameObject in level.LevelObjects[(int)SpawnPos.X-1][-(int)SpawnPos.Y])
                {
                    if (gameObject is Ground)
                    {
                        left = false;
                        topLeft = false;
                        bottomLeft = false;
                    }
                }
            if (level.CheckForObject((int)SpawnPos.X + 1, -(int)SpawnPos.Y))
                foreach (GameObject gameObject in level.LevelObjects[(int)SpawnPos.X+1][-(int)SpawnPos.Y])
                {
                    if (gameObject is Ground)
                    {
                        right = false;
                        topRight = false;
                        bottomRight = false;
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
            
        }

        public static void DrawIcon(GameTime gameTime, SpriteBatch spriteBatch, Rectangle rect)
        {
            spriteBatch.Draw(LoadedContent.ground, rect, new Rectangle(0, 0, 108, 108), Color.White);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera)
        {
            spritebach.Draw(image, new Rectangle((int)(Pos.X-camera.X), (int)(Pos.Y-camera.Y), (int)BlockWidth, (int)BlockWidth), new Rectangle(0, 0, 108, 108), Color.White, 0, new Vector2(), SpriteEffects.None, (float)DrawLayer.Ground/1000);
        }
    }
}
