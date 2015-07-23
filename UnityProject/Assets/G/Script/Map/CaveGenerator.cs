using System.Collections.Generic;
using UnityEngine;

enum CaveState
{
    Empty,
    Wall
}

class CaveGenerator : MonoBehaviour
{
    private int width;
    private int height;

    private CaveState[][] map;

    private void Start()
    {
        Generate(100, 100);
    }

    public void Generate(int width, int height)
    {
        this.width = width;
        this.height = height;

        map = new CaveState[width][];

        for (int i = 0; i < width; ++i)
        {
            map[i] = new CaveState[height];

            for(int j=0;j< height; ++j)
            {
                map[i][j] = CaveState.Wall;
            }
        }

        Dig();
    }

    private void Dig()
    {
        var initialMiner = new CaveMiner();
        initialMiner.Init(map, width / 2, height / 2);

        var minerList = new List<CaveMiner>();
        minerList.Add(initialMiner);

        while (minerList.Count != 0)
        {
            var addList = new List<CaveMiner>();
            var removeList = new List<CaveMiner>();

            foreach (var miner in minerList)
            {
                bool result = miner.Dig();
                if (result == false)
                {
                    removeList.Add(miner);
                }
            }

            foreach (var removeMiner in removeList)
            {
                minerList.Remove(removeMiner);
            }

            foreach (var miner in minerList)
            {
                var newMiner = miner.GenerateNew();
                if(newMiner != null)
                {
                    addList.Add(newMiner);
                }
            }

            foreach(var newMiner in addList)
            {
                minerList.Add(newMiner);
            }
        }

        for (int i = 0; i < map.Length; ++i)
        {
            for (int j = 0; j < map[i].Length; ++j)
            {
                GameObject obj = null;
                // Empty
                if(map[i][j] == CaveState.Empty)
                {
                    obj = Object.Instantiate(GameObject.Find("EmptyCave"));
                }
                // Wall
                else if(map[i][j] == CaveState.Wall)
                {
                    obj = Object.Instantiate(GameObject.Find("WallCave"));
                }

                obj.GetComponent<Transform>().localPosition = new Vector2(i, j);
            }
        }
    }
}