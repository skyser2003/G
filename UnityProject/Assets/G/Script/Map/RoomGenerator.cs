using System.Collections.Generic;
using UnityEngine;
using Math = System.Math;

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
        Generate(100, 100, new Vector2(15, 15), new Vector2(15, 15));
    }

    private System.Random rand = new System.Random();
    private int piece;
    private int width;
    private int height;
    private Vector2 startPos;
    private Vector2 endPos;

    public Room Generate(int width, int height, Vector2 startPos, Vector2 endPos)
    {
        var room = new Room(width, height);

        this.width = width;
        this.height = height;
        this.startPos = startPos;
        this.endPos = endPos;
        piece = 3;

        var sectorList = CreateSetorList();
        GeneratePlatforms(sectorList);

        return room;
    }

    private List<Sector> CreateSetorList()
    {
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

                if (direction == 0)
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

                if (tempI < 0 || tempJ < 0 || piece <= tempI || piece <= tempJ)
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

            if (finished == true)
            {
                break;
            }
        }

        sectorList.Add(endSector);

        return sectorList;
    }

    private void GeneratePlatforms(List<Sector> sectorList)
    {
        var fragment = new Vector2(width / piece, height / piece);

        for (int i = 0; i < sectorList.Count; ++i)
        {
            var sector = sectorList[i];
            var nextSector = i == sectorList.Count - 1 ? null : sectorList[i + 1];

            float leftX = fragment.x * sector.X;
            float bottomY = fragment.y * sector.Y;
            float rightX = fragment.x * (sector.X + 1);
            float topY = fragment.y * (sector.Y + 1);

            int nextX = nextSector == null ? 0 : Math.Sign(nextSector.X - sector.X);
            int nextY = nextSector == null ? 0 : Math.Sign(nextSector.Y - sector.Y);

            // Sector bg
            var sectorObj = Object.Instantiate(GameObject.Find("SectorBackground"));
            sectorObj.GetComponent<Transform>().localPosition = new Vector2(fragment.x * (sector.X + 0.5f), fragment.y * (sector.Y + 0.5f));
            sectorObj.GetComponent<Transform>().localScale = fragment;
            sectorObj.GetComponent<Renderer>().sortingOrder = -1;

            // Platform
            float destX = 0;
            float destY = 0;

            if (nextSector == null)
            {
                destX = leftX + endPos.x;
                destY = bottomY + endPos.y;
            }
            else if (nextX != 0)
            {
                destX = rightX;
            }
            else if (nextY == 1)
            {
                destY = topY;
            }
            else
            {
                destY = bottomY;
            }

            Transform prevPlatform = null;

            while (true)
            {
                var obj = Object.Instantiate(GameObject.Find("Platform"));
                obj.GetComponent<Renderer>().sortingOrder = 1;

                var platform = obj.AddComponent<Platform>();
                var transform = obj.GetComponent<Transform>();

                int platformWidth = rand.Next((int)fragment.x);
                platformWidth = Math.Max(platformWidth, 1);
                platformWidth = Math.Min(platformWidth, 5);

                float x;
                float y;
                bool finished = false;

                if (prevPlatform == null)
                {
                    x = rand.Next((int)(rightX - leftX)) + leftX;
                    y = rand.Next((int)(topY - bottomY)) + bottomY;
                }
                else
                {
                    // Next sector is on right
                    if (nextX != 0)
                    {
                        x = rand.Next((int)(rightX - prevPlatform.localPosition.x)) + prevPlatform.localPosition.x;
                        y = rand.Next(5) - 2 + prevPlatform.localPosition.y;

                        x = Math.Min(destX, x);

                        if (destX - x <= 1)
                        {
                            finished = true;
                        }
                    }
                    // Next sector is on top/bottom
                    else if(nextY != 0)
                    {
                        x = rand.Next((int)(rightX - leftX)) + leftX;
                        y = prevPlatform.localPosition.y + (rand.Next(2) + 1) * nextY;

                        if (nextY == -1)
                        {
                            y = Math.Max(bottomY, y);
                            if (y - bottomY <= 1)
                            {
                                finished = true;
                            }
                        }
                        else
                        {
                            y = Math.Min(topY, y);
                            if (topY - y <= 1)
                            {
                                finished = true;
                            }
                        }
                    }
                    // No next sector
                    else
                    {
                        var direction = new Vector2(Math.Sign(destX - prevPlatform.localPosition.x), Math.Sign(destY - prevPlatform.localPosition.y));

                        x = rand.Next((int)((destX - prevPlatform.localPosition.x) * direction.x)) * direction.x + prevPlatform.localPosition.x;
                        y = prevPlatform.localPosition.y + (rand.Next(2) + 1) * direction.y;

                        if (direction.y == -1)
                        {
                            y = Math.Max(destY, y);
                            if (y - destY <= 1)
                            {
                                finished = true;
                            }
                        }
                        else
                        {
                            y = Math.Min(destY, y);
                            if (destY - y <= 1)
                            {
                                finished = true;
                            }
                        }
                    }
                }

                transform.localPosition = new Vector2(x, y);
                transform.localScale = new Vector2(platformWidth, 1);

                prevPlatform = transform;

                if (finished == true)
                {
                    break;
                }
            }
        }
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