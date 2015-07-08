using System;
using System.Collections.Generic;
using UnityEngine;

class AttackObject : MonoBehaviour
{
    public AttackObjectDataRow info;
    private int baseDamage;
    private Vector2 speed;
    private int leftFrame;

    public void Init(int baseDamage, AttackObjectDataRow info, Vector2 position)
    {
        this.baseDamage = baseDamage;
        this.info = info;

        GetComponent<Transform>().localPosition = position;
        speed = info.MoveLocalSpeed_Vector3;
    }

    private void Update()
    {
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
        if (info == null)
        {
            return;
        }

        bool damageDealt = false;

        var unit = collider.gameObject.GetComponent<Unit>();
        if(unit == null)
        {
            return;
        }

        unit.GetDamage((int)(info.DamageMulti * baseDamage));
        damageDealt = true;


        if (damageDealt == true)
        {
            UnityEngine.Object.Destroy(gameObject);
        }
    }
}