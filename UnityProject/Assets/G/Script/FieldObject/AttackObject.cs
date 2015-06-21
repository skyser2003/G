using System;
using UnityEngine;

class AttackInfo
{
    public int ownerID;
    public Type[] targetGroup;
    public int damage;
}

class AttackObject : MonoBehaviour
{
    public AttackInfo info = new AttackInfo();

    public void Init(AttackInfo info)
    {

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
        }
    }
}