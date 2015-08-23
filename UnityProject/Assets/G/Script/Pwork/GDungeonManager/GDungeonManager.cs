using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class GDungeonManager : MonoBehaviour
{
    private static GDungeonManager instance;
    public static GDungeonManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(GDungeonManager)) as GDungeonManager;
            }

            return instance;
        }
    }

    // Use this for initialization
    void Start()
    {
        
    }

	public void Create()
	{
		while (path == null || path.Count > 10)
		{
			CreateDungeon();
		}
		
		PlaceInMap(path, "MAIN", null);
		
		for (int i = 0; i < subPathList.Count; ++i)
		{
			PlaceInMap(subPathList[i].path, "SUB", connectedMainPos[i]);
		}
		
		PlaceUnusedInMap(unusedPointList);
	}

    // Update is called once per frame
    void Update()
    {
    }

    [Serializable]
    public struct Path
    {
        public List<Vector2> path;
    }

    public GameObject CurCreatedDungeon;

    public List<GDungeonPart> DungeonPartList = new List<GDungeonPart>();
    public GDungeonPart AllClosedPart;
    public int DungeonWidth;
    public int DungeonHeight;
    private List<Vector2> path;
    private List<Path> subPathList;
    private List<Vector2> connectedMainPos;
    private List<Vector2> unusedPointList;
    private Vector2 offset = new Vector2(0, 0);

    public void CreateDungeon()
    {
        path = new List<Vector2>();

        // TODO : set begin position and destination position
        var begin = new Vector2(0, 0);
        var destination = new Vector2(4, 0);

        // Main path
        var curPos = begin;
        path.Add(curPos);

        while (true)
        {
            var possiblePathList = GetAllMoveablePath(curPos);
            if (possiblePathList.Contains(destination) == true)
            {
                break;
            }

            // Remove already visited path
            foreach (var point in path)
            {
                possiblePathList.Remove(point);
            }

            Vector2 randomWay;

            while (true)
            {
                int index = Random.Range(0, possiblePathList.Count);
                randomWay = possiblePathList[index];
                if (PathExists(path, randomWay, destination) == true)
                {
                    break;
                }
            }

            path.Add(randomWay);
            curPos = randomWay;
        }

        path.Add(destination);

        // Sub path
        const int maxSubPathCount = 2;
        const int maxSubPathLength = 3;
        subPathList = new List<Path>();
        connectedMainPos = new List<Vector2>();

        foreach (var point in path)
        {
            var subPath = new List<Vector2>();
            var curSubPos = point;

            while (true)
            {
                var possiblePathList = GetAllMoveablePath(curSubPos);
                foreach (var point2 in path)
                {
                    possiblePathList.Remove(point2);
                }
                foreach (var point2 in subPath)
                {
                    possiblePathList.Remove(point2);
                }
                foreach (var subPath2 in subPathList)
                {
                    foreach (var point2 in subPath2.path)
                    {
                        possiblePathList.Remove(point2);
                    }
                }

                if (possiblePathList.Count == 0)
                {
                    break;
                }

                var subPos = possiblePathList[Random.Range(0, possiblePathList.Count)];
                subPath.Add(subPos);

                if (maxSubPathLength <= subPath.Count)
                {
                    break;
                }

                curSubPos = subPos;
            }

            if (subPath.Count != 0)
            {
                subPathList.Add(new Path { path = subPath });
                connectedMainPos.Add(point);
            }

            if (maxSubPathCount <= subPathList.Count)
            {
                break;
            }
        }

        // Fill in unused points and walls
        unusedPointList = new List<Vector2>();
        for(int i = -1; i <= DungeonWidth; ++i)
        {
            for(int j = -1; j <= DungeonHeight; ++j)
            {
                unusedPointList.Add(new Vector2(i, j));
            }
        }

        foreach(var point in path)
        {
            unusedPointList.Remove(point);
        }
        foreach(var subPath in subPathList)
        {
            foreach(var point in subPath.path)
            {
                unusedPointList.Remove(point);
            }
        }
    }

    private void PlaceInMap(List<Vector2> path, string prefix, Vector2? initialPos)
    {
        var nextInput = Direction.LEFT;

        for (int i = 0; i < path.Count; ++i)
        {
            var pos = path[i];
            var inputList = new HashSet<Direction>();
            var outputList = new HashSet<Direction>();

            if (i == 0)
            {
                if (initialPos == null)
                {
                    inputList.Add(Direction.BOT);
                    inputList.Add(Direction.LEFT);
                    inputList.Add(Direction.RIGHT);
                    inputList.Add(Direction.TOP);
                }
                else
                {
                    var firstInput = Direction.LEFT;
                    var dir = pos - initialPos;

                    if (dir.Value.x == 0 && dir.Value.y == 1)
                    {
                        firstInput = Direction.BOT;
                    }
                    else if (dir.Value.x == 0 && dir.Value.y == -1)
                    {
                        firstInput = Direction.TOP;
                    }
                    else if (dir.Value.x == 1 && dir.Value.y == 0)
                    {
                        firstInput = Direction.LEFT;
                    }
                    else if (dir.Value.x == -1 && dir.Value.y == 0)
                    {
                        firstInput = Direction.RIGHT;
                    }

                    inputList.Add(firstInput);
                }
            }
            else
            {
                inputList.Add(nextInput);
            }

            if (i == path.Count - 1)
            {
                outputList.Add(Direction.LEFT);
                outputList.Add(Direction.BOT);
                outputList.Add(Direction.RIGHT);
                outputList.Add(Direction.TOP);
            }
            else
            {
                var nextPos = path[i + 1];
                var dir = nextPos - pos;

                if (dir.x == 0 && dir.y == 1)
                {
                    nextInput = Direction.BOT;
                    outputList.Add(Direction.TOP);
                }
                else if (dir.x == 0 && dir.y == -1)
                {
                    nextInput = Direction.TOP;
                    outputList.Add(Direction.BOT);
                }
                else if (dir.x == 1 && dir.y == 0)
                {
                    nextInput = Direction.LEFT;
                    outputList.Add(Direction.RIGHT);
                }
                else if (dir.x == -1 && dir.y == 0)
                {
                    nextInput = Direction.RIGHT;
                    outputList.Add(Direction.LEFT);
                }
            }

            var useablePartList = new List<GDungeonPart>();

            foreach (var part in DungeonPartList)
            {
                bool inputExists = false;
                bool outputExists = false;

                foreach (var input in inputList)
                {
                    if (part.InDirectionList.Contains(input) == true)
                    {
                        inputExists = true;
                        break;
                    }
                }

                foreach (var output in outputList)
                {
                    if (part.OutDirectionList.Contains(output) == true)
                    {
                        outputExists = true;
                        break;
                    }
                }

                if (inputExists == true && outputExists == true)
                {
                    useablePartList.Add(part);
                }
            }

            if (useablePartList.Count != 0)
            {
                var index = Random.Range(0, useablePartList.Count);
                var part = useablePartList[index];

                var type = prefix == "MAIN" ? PartType.MAIN : PartType.SIDE;
                var obj = UnityEngine.Object.Instantiate(part.gameObject);
                obj.GetComponent<Transform>().localPosition = offset + new Vector2(pos.x * 30, pos.y * 30);
                obj.GetComponent<GDungeonPart>().Create(type, i, path.Count);
                obj.name = pos.x + "-" + pos.y + ":" + prefix;
            }
        }
    }

    private void PlaceUnusedInMap(List<Vector2> unusedPointList)
    {
        foreach (var pos in unusedPointList)
        {
            var obj = UnityEngine.Object.Instantiate(AllClosedPart.gameObject);
            obj.GetComponent<Transform>().localPosition = offset + new Vector2(pos.x * 30, pos.y * 30);
            obj.name = pos.x + "-" + pos.y + ":" + "CLOSED";
        }
    }

    private List<Vector2> GetAllMoveablePath(Vector2 center)
    {
        var ret = new List<Vector2>();

        if (center.x > 0)
        {
            ret.Add(new Vector2(center.x - 1, center.y));
        }
        if (center.x < DungeonWidth - 1)
        {
            ret.Add(new Vector2(center.x + 1, center.y));
        }
        if (center.y > 0)
        {
            ret.Add(new Vector2(center.x, center.y - 1));
        }
        if (center.y < DungeonHeight - 1)
        {
            ret.Add(new Vector2(center.x, center.y + 1));
        }

        return ret;
    }

    private bool PathExists(List<Vector2> path, Vector2 begin, Vector2 end)
    {
        bool ret = false;

        var visited = new HashSet<Vector2>();
        var stack = new Stack<Vector2>();
        stack.Push(begin);

        while (stack.Count != 0)
        {
            if (visited.Contains(end) == true)
            {
                ret = true;
                break;
            }

            var pos = stack.Pop();
            if (visited.Contains(pos) == true)
            {
                continue;
            }

            visited.Add(pos);

            Action<float, float> addEdge = (float x, float y) =>
             {
                 var tempPos = new Vector2(x, y);
                 if (path.Contains(tempPos) == false && IsValid(x, y) == true && visited.Contains(tempPos) == false)
                 {
                     stack.Push(tempPos);
                 }
             };

            addEdge(pos.x - 1, pos.y);
            addEdge(pos.x + 1, pos.y);
            addEdge(pos.x, pos.y - 1);
            addEdge(pos.x, pos.y + 1);
        }

        return ret;
    }

    private bool IsValid(Vector2 pos)
    {
        return IsValid((int)pos.x, (int)pos.y);
    }

    private bool IsValid(float x, float y)
    {
        return IsValid((int)x, (int)y);
    }

    private bool IsValid(int x, int y)
    {
        return 0 <= x && x < DungeonWidth && 0 <= y && y < DungeonHeight;
    }
}
