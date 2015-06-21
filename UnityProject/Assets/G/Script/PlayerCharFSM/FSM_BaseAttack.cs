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
        var obj = UnityEngine.Object.Instantiate(GameObject.Find("AttackSphere"));
        var atkObj = obj.GetComponent<AttackObject>();
        var info = new AttackInfo();
        info.startPosition = pc.GetComponent<Transform>().localPosition;
        info.ownerID = pc.GetComponent<Unit>().UID;
        info.targetGroup.Add(typeof(Monster));
        info.totalFrame = 3;
        info.damagePerFrame = 500;
        info.beginSpeed = new Vector2(1.0f * pc.GetComponent<Transform>().rotation.y, 0);
        info.accel = new Vector2(2.0f * pc.GetComponent<Transform>().rotation.y, 0);

        atkObj.Init(info);
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