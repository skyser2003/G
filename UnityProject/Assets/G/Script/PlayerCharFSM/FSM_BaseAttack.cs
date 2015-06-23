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

        var info = DataManager.Inst.GetAttackInfo("Fireball").Clone();
        info.startPosition.x += pc.GetComponent<Transform>().localPosition.x;
        info.startPosition.y += pc.GetComponent<Transform>().localPosition.y;
        info.ownerID = pc.GetComponent<Unit>().UID;
        info.targetGroup.Add(typeof(Monster));
        info.initialSpeed.x *= pc.GetComponent<Transform>().rotation.y;
        info.acceleration.x *= pc.GetComponent<Transform>().rotation.y;

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

        pc.Stop();
    }

    override public void OnEnd()
    {
    }
}