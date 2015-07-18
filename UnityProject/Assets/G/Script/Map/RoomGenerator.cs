using System.Collections.Generic;
using UnityEngine;

class RoomGenerator : MonoBehaviour
{
    private class Sector
    {
        private int x;
        private int y;

        public int X { get { return x; } }
        public int Y { get { return y; } }

        public void Init(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    private void Start()
    {
        Generate(100, 100);
    }

    private System.Random rand = new System.Random();

    public Room Generate(int width, int height)
    {
        var room = new Room(width, height);

        int piece = 3;
        var fragment = new Vector2(width / piece, height / piece);

        var beginY = rand.Next(piece);
        var endY = rand.Next(piece);

        var beginSector = new Sector();
        var endSector = new Sector();
        beginSector.Init(0, beginY);
        endSector.Init(piece - 1, endY);

        var sectorList = new List<Sector>();
        sectorList.Add(beginSector);

        int i = beginSector.X;
        int j = beginSector.Y;

        while (true)
        {
            var visited = new HashSet<int>();
            bool impossible = false;
            bool finished = false;

            while (true)
            {
                var direction = rand.Next(3);

                if (visited.Count == 3)
                {
                    impossible = true;
                    break;
                }

                if (visited.Contains(direction))
                {
                    continue;
                }

                int tempI = i;
                int tempJ = j;

                if(direction == 0)
                {
                    ++tempJ;
                }
                else if (direction == 1)
                {
                    ++tempI;
                }
                else if (direction == 2)
                {
                    --tempJ;
                }

                if (tempI == endSector.X && tempJ == endSector.Y)
                {
                    finished = true;
                    break;
                }

                if (piece <= tempI || piece <= tempJ)
                {
                    visited.Add(direction);
                    continue;
                }

                if (Contains(sectorList, tempI, tempJ) == true)
                {
                    visited.Add(direction);
                    continue;
                }

                var sector = new Sector();
                sector.Init(tempI, tempJ);
                sectorList.Add(sector);

                i = tempI;
                j = tempJ;
                break;
            }

            if (impossible == true)
            {
                sectorList.RemoveAt(sectorList.Count - 1);
                i = sectorList[sectorList.Count - 1].X;
                j = sectorList[sectorList.Count - 1].Y;
                continue;
            }

            if(finished == true)
            {
                break;
            }
        }

        sectorList.Add(endSector);

        return room;
    }

    private bool Contains(List<Sector> sectorList, int x, int y)
    {
        foreach (var sector in sectorList)
        {
            if (sector.X == x && sector.Y == y)
            {
                return true;
            }
        }

        return false;
    }
}