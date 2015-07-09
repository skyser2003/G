using System;
using System.Collections.Generic;
using UnityEngine;

class AttackObject : MonoBehaviour
{
    private bool isRealObject = false;
    private int baseDamage;
    private Vector2 speed;
    private int remainFrame;

    public AttackObjectDataRow info;

    public void Init(int baseDamage, AttackObjectDataRow info, Vector3 position, int direction)
    {
        this.baseDamage = baseDamage;
        this.info = info;

        GetComponent<Transform>().position = new Vector2(position.x + info.CreateDeltaPos_Vector3.x * direction, position.y + info.CreateDeltaPos_Vector3.y);
        speed = info.MoveLocalSpeed_Vector3;
        remainFrame = info.RemainFrame;
        isRealObject = true;
    }

    private void Update()
    {
        if (isRealObject == false)
        {
            return;
        }

        --remainFrame;
        if (remainFrame <= 0)
        {
            UnityEngine.Object.Destroy(gameObject);
        }

        if (info == null)
        {
            return;
        }

        var dt = speed * Time.deltaTime;
        var newPos = GetComponent<Transform>().localPosition;
        newPos.x += dt.x;
        newPos.y += dt.y;
        GetComponent<Transform>().localPosition = newPos;
    }

    private void OnTriggerStay(Collider collider)
    {
        if (isRealObject == false)
        {
            return;
        }

        if (info == null)
        {
            return;
        }

        var unit = collider.gameObject.GetComponent<Unit>();
        if (unit == null)
        {
            return;
        }

        unit.GetDamage((int)(info.DamageMulti * baseDamage));
    }
}