using System;
using System.Collections.Generic;
using UnityEngine;

class AttackObject : MonoBehaviour
{
    public AttackObjectInfo info;
    private Vector2 speed;
    private int leftFrame;

    public void Init(AttackObjectInfo info)
    {
        this.info = info;

        GetComponent<Transform>().localPosition = info.position;
        speed = info.initialSpeed;
    }

    private void Update()
    {
        if (info == null)
        {
            return;
        }

        var ds = info.acceleration * Time.deltaTime;
        speed += ds;

        var dt = speed * Time.deltaTime;
        var newPos = GetComponent<Transform>().localPosition;
        newPos.x += dt.x;
        newPos.y += dt.y;
        GetComponent<Transform>().localPosition = newPos;
    }

    private void OnTriggerStay(Collider collider)
    {
        if (info == null)
        {
            return;
        }

        bool damageDealt = false;

        foreach (var type in info.targetGroup)
        {
            var target = collider.gameObject.GetComponent(type);
            if (target == null)
            {
                continue;
            }

            var unit = collider.gameObject.GetComponent<Unit>();
            unit.GetDamage(info.damage);
            damageDealt = true;
            break;
        }

        if (damageDealt == true)
        {
            UnityEngine.Object.Destroy(gameObject);
        }
    }
}