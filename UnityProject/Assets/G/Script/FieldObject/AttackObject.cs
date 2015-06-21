using System;
using System.Collections.Generic;
using UnityEngine;

class AttackInfo
{
    public Vector3 startPosition;
    public int ownerID;
    public List<Type> targetGroup = new List<Type>();
    public int totalFrame;
    public int damagePerFrame;
    public float beginSpeed;
    public float accel;
}

class AttackObject : MonoBehaviour
{
    private AttackInfo info;
    private float speed;
    private int leftFrame;

    public void Init(AttackInfo info)
    {
        this.info = info;

        GetComponent<Transform>().localPosition = info.startPosition;
        speed = info.beginSpeed;
        leftFrame = info.totalFrame;
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
            unit.GetDamage(info.damagePerFrame);
            damageDealt = true;
            break;
        }

        if (damageDealt == true)
        {
            --leftFrame;
            if (leftFrame <= 0)
            {
                UnityEngine.Object.Destroy(gameObject);
            }
        }
    }
}