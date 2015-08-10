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

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject CurCreatedDungeon;

    public List<GDungeonPart> DungeonPartList = new List<GDungeonPart>();
    public int DungeonWidth;
    public int DungeonHeight;
    List<Vector2> path;

    public void CreateDungeon()
    {
        //make path of 16
        //start is left_bot and end is right_bot
        //set main path first.
        //then create sub paths.
        //
        // height - 1
        // .
        // width + 1 ....
        // 0  1  2  ... width

        path = new List<Vector2>();

        // TODO : set begin position and destination position
        var begin = new Vector2();
        var destination = new Vector2();

        // Main path
        var curPos = begin;

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
                randomWay = possiblePathList[Random.Range(0, possiblePathList.Count)];
                if (PathExists(path, randomWay, destination) == true)
                {
                    break;
                }
            }

            path.Add(randomWay);
        }

        path.Add(destination);

        // TODO : Sub path
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
            if(visited.Contains(end) == true)
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
