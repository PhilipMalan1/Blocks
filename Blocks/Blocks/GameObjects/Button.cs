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
        private int rotation;
        int doorX, doorY;

        [NonSerialized]
        private Body body;
        [NonSerialized]
        private Texture2D[] images;
        [NonSerialized]
        private float height;
        [NonSerialized]
        private State state;
        [NonSerialized]
        private int animationTimer;
        [NonSerialized]
        private int animationSpeed;
        [NonSerialized]
        private int frame;

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

        public State State1
        {
            get
            {
                return state;
            }

            set
            {
                if (state != value)
                {
                    animationTimer = 1;
                    if (value == State.Pressed && door!=null)
                        door.Open();
                }
                state = value;
            }
        }

        private Door door
        {
            get
            {
                try
                {
                    Door door=null;
                    foreach (GameObject gameObject in Level.LevelObjects[doorX][doorY])
                    {
                        if (gameObject is Door)
                            door = (Door)gameObject;
                    }
                    return door;
                } catch (Exception) { }
                return null;
            }
        }

        public override string DataValueName()
        {
            return "Object: Button DataValue: rotation Door: ("+doorX+", "+doorY+")";
        }

        public static void DrawIcon(GameTime gameTime, SpriteBatch spriteBatch, Rectangle rect, float blockWidth)
        {
            Texture2D image = LoadedContent.button[0];
            float height = image.Height * blockWidth / image.Width;
            spriteBatch.Draw(image, new Rectangle(rect.X, (int)(rect.Y+blockWidth-height), (int)blockWidth, (int)height), new Rectangle(0, 0, image.Width, image.Height), Color.White, 0, new Vector2(), SpriteEffects.None, (float)DrawLayer.UI / 1000);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera)
        {
            Texture2D image = images[frame];
            Vector2 origin = new Vector2(image.Width / 2, -image.Width / 2 + image.Height);
            spritebach.Draw(image, new Rectangle((int)(Pos.X - camera.X + BlockWidth / 2), (int)(Pos.Y + BlockWidth - height - camera.Y - height), (int)BlockWidth, (int)height), new Rectangle(0, 0, image.Width, image.Height), Color.White, (float)Math.PI / 2 * rotation, origin, SpriteEffects.None, (float)DrawLayer.Button/1000);
        }

        public override void Initialize(Level level, float blockWidth)
        {
            Level = level;
            BlockWidth = blockWidth;

            if (door == null)
            {
                doorX = (int)SpawnPos.X + 2;
                doorY = -(int)SpawnPos.Y;
                Door door = new Door(level, blockWidth, new Vector2(doorX, -doorY));
                level.AddGameObject(door, doorX, doorY);
            }
            door.SetButton((int)SpawnPos.X, -(int)SpawnPos.Y);

            animationSpeed = 3;
            animationTimer = 0;
            State1 = State.Unpressed;
            frame = 0;
            images = LoadedContent.button;
            height = images[0].Height * blockWidth / images[0].Width;

            body = new Body(this, level.PhysicsMangager, true, 1, 0, 0);
            body.Pos = SpawnPos * BlockWidth;
            UpdateRotation();
        }

        private void UpdateRotation()
        {
            Func<CollisionData, bool> onCollision = collisionData =>
              {
                  GameObject other = collisionData.OtherCollider.Body.GameObject;

                  if (other is Block)
                  {
                      State1 = State.Pressed;
                  }
                  return false;
              };

            if (rotation == 0)
                body.AddCollider(new RectangleCollider(body, CollisionGroup.Ground, new Vector2(0, BlockWidth - height), new Vector2(BlockWidth, height), onCollision));
            else if (rotation == 1)
                body.AddCollider(new RectangleCollider(body, CollisionGroup.Ground, new Vector2(0, 0), new Vector2(height, BlockWidth), onCollision));
            else if (rotation == 2)
                body.AddCollider(new RectangleCollider(body, CollisionGroup.Ground, new Vector2(0, 0), new Vector2(BlockWidth, height), onCollision));
            else if (rotation == 3)
                body.AddCollider(new RectangleCollider(body, CollisionGroup.Ground, new Vector2(BlockWidth-height, 0), new Vector2(height, BlockWidth), onCollision));
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
            if (State1==State.Pressed)
            {
                frame = 1 + animationTimer / animationSpeed;
                if (frame >= images.Count())
                    frame = images.Count() - 1;
                animationTimer++;
            }
        }

        public override void Load()
        {
            body.Load();
        }

        public override void Unload()
        {
            body.Unload();
        }

        public enum State
        {
            Unpressed,
            Pressed
        }

        public void SetDoor(int x, int y)
        {
            doorX = x;
            doorY = y;
        }

        public override void Move(int x, int y)
        {
            base.Move(x, y);
            door.SetButton(x, y);
        }
    }
}
