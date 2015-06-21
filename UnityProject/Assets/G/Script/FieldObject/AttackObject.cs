﻿using System;
using System.Collections.Generic;
using UnityEngine;

class AttackInfo
{
    public Vector3 startPosition;
    public int ownerID;
    public List<Type> targetGroup = new List<Type>();
    public int damage;
    public float beginSpeed;
    public float accel;
}

class AttackObject : MonoBehaviour
{
    private AttackInfo info;
    private float speed;

    public void Init(AttackInfo info)
    {
        this.info = info;

        GetComponent<Transform>().localPosition = info.startPosition;
        speed = info.beginSpeed;
    }

    private void Update()
    {
        if (info == null)
        {
            return;
        }

        float ds = info.accel * Time.deltaTime;
        speed += ds;

        float dt = speed * Time.deltaTime;
        var newPos = GetComponent<Transform>().localPosition;
        newPos.x += dt;
        GetComponent<Transform>().localPosition = newPos;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (info == null)
        {
            return;
        }

        foreach (var type in info.targetGroup)
        {
            var target = collider.gameObject.GetComponent(type);
            if (target == null)
            {
                continue;
            }

            var unit = collider.gameObject.GetComponent<Unit>();
            unit.GetDamage(info.damage);
            UnityEngine.Object.Destroy(gameObject);

            break;
        }
    }
}