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
                    collider.GetComponent<Unit>().GetDamage(500);
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