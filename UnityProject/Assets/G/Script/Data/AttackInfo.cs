using System;
using System.Collections.Generic;
using UnityEngine;

class AttackFrameInfo
{
    public Vector2 position;
    public double time;
    public int damage;

    public AttackFrameInfo Clone()
    {
        var ret = new AttackFrameInfo();
        ret.position = new Vector2(position.x, position.y);
        ret.time = time;
        ret.damage = damage;

        return ret;
    }
}

class AttackObjectInfo
{
    public Vector2 position;
    public Vector2 initialSpeed;
    public Vector2 acceleration;
    public int damage;

    public List<Type> targetGroup = new List<Type>();
    public int ownerID;
}

class AttackInfo
{
    // Static data from json
    public string name;
    public AttackFrameInfo[] frames;
    public Vector2 initialSpeed;
    public Vector2 acceleration;

    // Runtime data
    public List<Type> targetGroup = new List<Type>();
    public int ownerID;

    public AttackInfo Clone()
    {
        var ret = new AttackInfo();
        ret.name = name;
        ret.frames = new AttackFrameInfo[frames.Length];
        for (int i = 0; i < frames.Length; ++i)
        {
            ret.frames[i] = frames[i].Clone();
        }
        ret.initialSpeed = initialSpeed;
        ret.acceleration = acceleration;

        ret.targetGroup = new List<Type>(targetGroup);
        ret.ownerID = ownerID;

        return ret;
    }
}