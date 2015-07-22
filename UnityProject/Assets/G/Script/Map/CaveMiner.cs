using UnityEngine;

class CaveMiner
{
    private CaveState[][] map;
    private System.Random rand = new System.Random();
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

        var direction = rand.Next(4);
        int originalX = x;
        int originalY = y;

        while (true)
        {
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

            if (IsValid(x, y) == false)
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
        var possibility = rand.Next(100);
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
        if (IsValid(x - 1, y) == false || map[x - 1][y] == CaveState.Wall)
        {
            return false;
        }
        if (IsValid(x + 1, y) == false || map[x + 1][y] == CaveState.Wall)
        {
            return false;
        }
        if (IsValid(x, y - 1) == false || map[x][y - 1] == CaveState.Wall)
        {
            return false;
        }
        if (IsValid(x, y + 1) == false || map[x][y + 1] == CaveState.Wall)
        {
            return false;
        }

        return true;
    }

    private bool IsValid(int x, int y)
    {
        return 0 <= x && x < map.Length && 0 <= y && y < map[0].Length;
    }
}