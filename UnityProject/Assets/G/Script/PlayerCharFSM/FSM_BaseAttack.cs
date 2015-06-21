using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class FSM_BaseAttack : AbstractFSM
{
    private float attackTime = 1.0f;

    override public void OnBegin()
    {
        Collider[] colliders;
        if ((colliders = Physics.OverlapSphere(pc.GetComponent<Transform>().position, 100.0f)).Length > 0)
        {
            foreach (var collider in colliders)
            {
                var monster = collider.GetComponent<Monster>();
                if (monster != null)
                {
                    var obj = UnityEngine.Object.Instantiate(GameObject.Find("AttackSphere"));
                    var atkObj = obj.GetComponent<AttackObject>();
                    var info = new AttackInfo();
                    info.startPosition = pc.GetComponent<Transform>().localPosition;
                    info.ownerID = pc.GetComponent<Unit>().UID;
                    info.targetGroup.Add(typeof(Monster));
                    info.damage = 500;

                    atkObj.Init(info);
                }
            }
        }
    }

    override public void OnUpdate()
    {
        attackTime -= Time.deltaTime;
        if (attackTime <= 0.0f)
        {
            pc.SetState(STATE.IDLE);
            return;
        }

        if (pc.WalkVelocity != 0.0f)
        {
            pc.WalkVelocity = pc.Walk.Direction * (pc.WalkSpeed - Time.deltaTime);
            if (pc.WalkVelocity < 0.0f)
            {
                pc.WalkVelocity = 0.0f;
            }
        }
    }

    override public void OnEnd()
    {
    }
}