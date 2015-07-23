using System;
using System.Collections.Generic;
using UnityEngine;

class CaveMiner
{
    private CaveState[][] map;
    private int x, y;

    public void Init(CaveState[][] map, int x, int y)
    {
        this.map = map;
        this.x = x;
        this.y = y;
    }

    public bool Dig()
    {
        if (FourSidesUndiggable(x, y) == true)
        {
            return false;
        }

        var tempList = new List<int>();

        while (true)
        {
            int originalX = x;
            int originalY = y;

            int direction = UnityEngine.Random.Range(0, 4);
            switch (direction)
            {
                // Up
                case 0:
                    {
                        ++y;
                    }
                    break;
                // Right
                case 1:
                    {
                        ++x;
                    }
                    break;
                // Down
                case 2:
                    {
                        --y;
                    }
                    break;
                // Left
                case 3:
                    {
                        --x;
                    }
                    break;
            }

            tempList.Add(direction);

            if (IsDiggable(x, y) == false)
            {
                x = originalX;
                y = originalY;
                continue;
            }
            else
            {
                break;
            }
        }

        map[x][y] = CaveState.Empty;

        return true;
    }

    public CaveMiner GenerateNew()
    {
        var possibility = UnityEngine.Random.Range(0, 100);
        if (possibility <= 7)
        {
            var miner = new CaveMiner();
            miner.Init(map, x, y);
            return miner;
        }
        else
        {
            return null;
        }
    }

    private bool FourSidesUndiggable(int x, int y)
    {
        if (IsDiggable(x - 1, y) == true ||
            IsDiggable(x + 1, y) == true ||
            IsDiggable(x, y - 1) == true ||
            IsDiggable(x, y + 1) == true)
        {
            return false;
        }

        return true;
    }

    private bool IsDiggable(int x, int y)
    {
        return IsValid(x, y) == true && map[x][y] == CaveState.Wall;
    }

    private bool IsValid(int x, int y)
    {
        return 0 <= x && x < map.Length && 0 <= y && y < map[0].Length;
    }
}