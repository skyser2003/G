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

    private Vector2 begin, end;

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
        }

        Dig();
    }

    private void InitMap()
    {
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                map[i][j] = CaveState.Wall;
            }
        }
    }

    private void InitMiners(List<CaveMiner> minerList)
    {
        var miner1 = new CaveMiner();
        var miner2 = new CaveMiner();

        miner1.Init(map, (int)begin.x, (int)begin.y);
        miner2.Init(map, (int)end.x, (int)end.y);

        minerList.Add(miner1);
        minerList.Add(miner2);
    }

    private void GenerateMap()
    {
        var minerList = new List<CaveMiner>();
        InitMiners(minerList);

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
                if (newMiner != null)
                {
                    addList.Add(newMiner);
                }
            }

            foreach (var newMiner in addList)
            {
                minerList.Add(newMiner);
            }
        }
    }

    private void Dig()
    {
        begin = new Vector2(width / 2, 0);
        end = new Vector2(width - 1, height / 2);

        while (true)
        {
            InitMap();
            GenerateMap();

            var visited = new bool[map.Length][];
            for (int i = 0; i < map.Length; ++i)
            {
                visited[i] = new bool[map[i].Length];
                for (int j = 0; j < map[i].Length; ++j)
                {
                    visited[i][j] = false;
                }
            }

            // Clear filled chunks smaller than 5 in size
            var chunkList = new List<List<Vector2>>();

            for (int i = 0; i < map.Length; ++i)
            {
                for (int j = 0; j < map[i].Length; ++j)
                {
                    if (map[i][j] != CaveState.Wall || visited[i][j] == true)
                    {
                        continue;
                    }

                    var chunk = new List<Vector2>();
                    GetChunk(CaveState.Wall, chunk, visited, i, j);
                    chunkList.Add(chunk);
                }
            }

            foreach (var chunk in chunkList)
            {
                if (chunk.Count <= 4)
                {
                    foreach (var point in chunk)
                    {
                        map[(int)point.x][(int)point.y] = CaveState.Empty;
                    }
                }
            }

            // Clear up
            for (int i = 0; i < map.Length; ++i)
            {
                for (int j = 0; j < map[i].Length; ++j)
                {
                    visited[i][j] = false;
                }
            }

            chunkList.Clear();

            // Check if path exists from start to end
            for (int i = 0; i < map.Length; ++i)
            {
                for (int j = 0; j < map[i].Length; ++j)
                {
                    if (map[i][j] != CaveState.Empty || visited[i][j] == true)
                    {
                        continue;
                    }

                    var chunk = new List<Vector2>();
                    GetChunk(CaveState.Empty, chunk, visited, i, j);
                    chunkList.Add(chunk);
                }
            }

            bool pathExists = false;
            foreach (var chunk in chunkList)
            {
                int counter = 2;
                foreach (var point in chunk)
                {
                    if (point == begin || point == end)
                    {
                        --counter;
                    }
                }

                if (counter == 0)
                {
                    pathExists = true;
                    break;
                }
            }

            // Restart the process
            if (pathExists == true)
            {
                break;
            }
        }

        for (int i = 0; i < map.Length; ++i)
        {
            for (int j = 0; j < map[i].Length; ++j)
            {
                GameObject obj = null;
                // Empty
                if (map[i][j] == CaveState.Empty)
                {
                    obj = Object.Instantiate(GameObject.Find("EmptyCave"));
                }
                // Wall
                else if (map[i][j] == CaveState.Wall)
                {
                    obj = Object.Instantiate(GameObject.Find("WallCave"));
                }

                obj.GetComponent<Transform>().localPosition = new Vector2(i, j);
            }
        }
    }

    private void GetChunk(CaveState state, List<Vector2> chunk, bool[][] visited, int x, int y)
    {
        if ((0 <= x && x < map.Length && 0 <= y && y < map[0].Length) == false)
        {
            return;
        }

        if (map[x][y] != state)
        {
            return;
        }

        if (visited[x][y] == false)
        {
            visited[x][y] = true;
            chunk.Add(new Vector2(x, y));
            GetChunk(state, chunk, visited, x - 1, y);
            GetChunk(state, chunk, visited, x + 1, y);
            GetChunk(state, chunk, visited, x, y - 1);
            GetChunk(state, chunk, visited, x, y + 1);
        }
    }
}