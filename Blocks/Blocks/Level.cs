using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    [Serializable]
    public class Level
    {
        private List<List<List<GameObject>>> levelObjects;
        private int rowNum;
        [NonSerialized]
        private PhysicsManager physicsMangager;
        [NonSerialized]
        private LoadManager loadManager;
        [NonSerialized]
        private List<IInput> inputObjects;
        [NonSerialized]
        private GameObject cameraFocus;
        [NonSerialized]
        private List<GameObject> loaded;
        [NonSerialized]
        private float blockWidth;
        [NonSerialized]
        private Vector2 camera;
        [NonSerialized]
        private GraphicsDevice graphicsDevice;

        public Level(List<List<List<GameObject>>> levelObjects, int rowNum, float blockWidth, GraphicsDevice graphicsDevice)
        {
            this.levelObjects = levelObjects;
            this.rowNum = rowNum;

            Initialize(blockWidth, graphicsDevice);
        }

        public void Initialize(float blockWidth, GraphicsDevice graphicsDevice)
        {
            this.blockWidth = blockWidth;
            this.graphicsDevice = graphicsDevice;

            physicsMangager = new PhysicsManager();
            loadManager = new LoadManager(this, blockWidth);

            inputObjects = new List<IInput>();
            foreach (List<List<GameObject>> column in LevelObjects)
            {
                foreach (List<GameObject> tile in column)
                {
                    foreach (GameObject gameObject in tile)
                    {
                        gameObject.Initialize(this, blockWidth);
                        if (gameObject is IInput)
                            inputObjects.Add((IInput)gameObject);
                        if (gameObject is ICameraFocus)
                            cameraFocus = gameObject;
                    }
                }
            }

            if(CameraFocus!=null) camera = CameraFocus.Pos + new Vector2(blockWidth / 2, blockWidth / 2) - new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);
            if (camera.X < 0)
                camera.X = 0;

            loaded = LoadManager.LoadFirstScreen(new Rectangle((int)camera.X, (int)camera.Y, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height));
        }

        public void Reset()
        {
            Initialize(blockWidth, graphicsDevice);
        }

        public void Update(GameTime gameTime)
        {
            //move camera
            Vector2 destination = CameraFocus.Pos + new Vector2(blockWidth / 2, blockWidth / 2) - new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);
            if (destination.X < 0)
                destination.X = 0;
            if (destination.Y > -graphicsDevice.Viewport.Height)
                destination.Y = -graphicsDevice.Viewport.Height;
            camera += (destination - camera) / 5;

            //load/unload
            LoadManager.Update(new Rectangle((int)camera.X, (int)camera.Y, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height), loaded);
        }

        public int RowNum
        {
            get
            {
                return rowNum;
            }
        }

        public List<List<List<GameObject>>> LevelObjects
        {
            get
            {
                return levelObjects;
            }

            set
            {
                levelObjects = value;
            }
        }

        public PhysicsManager PhysicsMangager
        {
            get
            {
                return physicsMangager;
            }

            set
            {
                physicsMangager = value;
            }
        }

        internal List<IInput> InputObjects
        {
            get
            {
                return inputObjects;
            }

            set
            {
                inputObjects = value;
            }
        }

        internal GameObject CameraFocus
        {
            get
            {
                return cameraFocus;
            }

            set
            {
                cameraFocus = value;
            }
        }

        public LoadManager LoadManager
        {
            get
            {
                return loadManager;
            }

            set
            {
                loadManager = value;
            }
        }

        public List<GameObject> Loaded
        {
            get
            {
                return loaded;
            }

            set
            {
                loaded = value;
            }
        }

        public Vector2 Camera
        {
            get
            {
                return camera;
            }

            set
            {
                camera = value;
            }
        }

        public bool CheckForObject(int x, int y)
        {
            if (x < 0 || y < 0)
                return false;
            if (x<levelObjects.Count() && y<levelObjects[x].Count())
            {
                return levelObjects[x][y].Count() > 0;
            }
            return false;
        }

        public void AddGameObject(GameObject gameObject, int x, int y)
        {
            if (x < 0) return;
            if (y < 0) return;

            //add columns
            while (x >= levelObjects.Count())
                levelObjects.Add(new List<List<GameObject>>());
            //add rows
            while (y >= levelObjects[x].Count())
            {
                levelObjects[x].Add(new List<GameObject>());
                if (levelObjects.Count() > rowNum) rowNum = levelObjects.Count();
            }
            //add object
            levelObjects[x][y].Add(gameObject);
        }

        public void RemoveGameObject(int x, int y)
        {
            //if object exists
            if (x<levelObjects.Count() && y<levelObjects[x].Count() && LevelObjects[x][y].Count>0)
            {
                //remove object
                levelObjects[x][y].RemoveAt(levelObjects[x][y].Count() - 1);
                //remove rows
                while (levelObjects[x].Count()>0 && levelObjects[x][levelObjects[x].Count() - 1].Count() <= 0)
                    levelObjects[x].Remove(levelObjects[x][levelObjects[x].Count() - 1]);
                //remove columns
                while (levelObjects.Count()>0 && levelObjects[levelObjects.Count() - 1].Count() <= 0)
                    levelObjects.Remove(levelObjects[levelObjects.Count() - 1]);
            }
        }
    }
}
