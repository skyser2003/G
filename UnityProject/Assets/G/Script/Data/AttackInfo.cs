using System;
using System.Collections.Generic;
using UnityEngine;

class AttackInfo
{
    // Static data from json
    public string name;
    public Vector2 startPosition;
    public int totalFrame;
    public int damagePerFrame;
    public Vector2 initialSpeed;
    public Vector2 acceleration;

    // Runtime data
    public List<Type> targetGroup = new List<Type>();
    public int ownerID;

    public AttackInfo Clone()
    {
        var ret = new AttackInfo();
        ret.name = name;
        ret.startPosition = startPosition;
        ret.totalFrame = totalFrame;
        ret.damagePerFrame = damagePerFrame;
        ret.initialSpeed = initialSpeed;
        ret.acceleration = acceleration;

        ret.targetGroup = new List<Type>(targetGroup);
        ret.ownerID = ownerID;

        return ret;
    }
}