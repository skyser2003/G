using UnityEngine;

class Room
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Room(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public void AddPlatform(Vector2 center, float width, float height)
    {
        var obj = new GameObject();
        var platform = obj.AddComponent<Platform>();
        obj.GetComponent<Transform>().localScale = new Vector3(width, height, 0);
    }
}