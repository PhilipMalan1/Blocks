using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocks
{
    [Serializable]
    class Door : GameObject
    {
        int buttonX, buttonY;

        [NonSerialized]
        Texture2D image;
        [NonSerialized]
        Body body;
        [NonSerialized]
        float height;
        [NonSerialized]
        float percentClosed;
        [NonSerialized]
        State state;

        public Door(Level level, float blockWidth, Vector2 spawnPos) : base(level, blockWidth, spawnPos)
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

        private State State1
        {
            get
            {
                return state;
            }

            set
            {
                if(value==State.Opening)
                {
                    percentClosed = 1;
                }
                if (value == State.Closed)
                {
                    percentClosed = 1;
                }
                if (value == State.Open)
                {
                    percentClosed = 0;
                }
                state = value;
            }
        }

        private Button button
        {
            get
            {
                try
                {
                    Button button = null;
                    foreach (GameObject gameObject in Level.LevelObjects[buttonX][buttonY])
                    {
                        if (gameObject is Button)
                            button = (Button)gameObject;
                    }
                    return button;
                }
                catch (Exception) { }
                return null;
            }
        }

        public override string DataValueName()
        {
            return "Object: Door DataValue: N/A";
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebach, Vector2 camera)
        {
            spritebach.Draw(image, new Rectangle((int)(Pos.X - camera.X), (int)(Pos.Y - camera.Y), (int)BlockWidth, (int)(height*percentClosed)), new Rectangle(0, (int)(image.Height*(1-percentClosed)), image.Width, (int)(image.Height*percentClosed)), Color.White, 0, new Vector2(), SpriteEffects.None, (float)DrawLayer.Door / 1000);
        }

        public override void Initialize(Level level, float blockWidth)
        {
            Level = level;
            BlockWidth = blockWidth;
            image = LoadedContent.door;
            height = blockWidth * 2;
            body = new Body(this, level.PhysicsMangager, true, 1, 0, 0);
            body.Pos = SpawnPos * blockWidth;
            body.Vel = new Vector2();
            body.AddCollider(new RectangleCollider(body, CollisionGroup.Ground, new Vector2(0, 0), new Vector2(blockWidth, height), collisionData => true));

            State1 = State.Closed;
        }

        private void UpdateCollider()
        {
            body.Colliders.RemoveAt(0);
            body.AddCollider(new RectangleCollider(body, CollisionGroup.Ground, new Vector2(0, 0), new Vector2(BlockWidth, height*percentClosed), collisionData => true));
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
            if(state==State.Opening)
            {
                percentClosed -= .1f;
                if (percentClosed <= 0)
                    state = State.Open;
                UpdateCollider();
            }
        }

        public void Open()
        {
            state = State.Opening;
        }

        public void Close()
        {
            state = State.Closed;
        }

        public void SetButton(int x, int y)
        {
            buttonX = x;
            buttonY = y;
        }

        public override void Move(int x, int y)
        {
            base.Move(x, y);
            button.SetDoor(x, y);
        }

        private enum State
        {
            Closed,
            Opening,
            Open
        }
    }
}
