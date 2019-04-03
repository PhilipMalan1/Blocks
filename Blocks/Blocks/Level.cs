using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    [Serializable]
    class Level
    {
        private List<List<List<GameObject>>> levelObjects;
        private int rowNum;
        [NonSerialized]
        private PhysicsManager physicsMangager;

        public Level(List<List<List<GameObject>>> levelObjects, int rowNum)
        {
            this.levelObjects = levelObjects;
            this.rowNum = rowNum;

            Initialize();
        }

        public void Initialize()
        {
            physicsMangager = new PhysicsManager();
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

        public bool CheckForObject(int x, int y)
        {
            if(x<levelObjects.Count() && y<levelObjects[x].Count())
            {
                return levelObjects[x][y].Count() > 0;
            }
            return false;
        }

        public void AddGameObject(GameObject gameObject, int x, int y)
        {
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
