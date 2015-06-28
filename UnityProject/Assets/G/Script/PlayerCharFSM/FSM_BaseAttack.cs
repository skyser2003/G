using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class FSM_BaseAttack : AbstractFSM
{
    private float preDelay;
    private float postDelay;
    bool attacked;

    override public void OnBegin()
    {
        preDelay = pc.attackPreDelay;
        postDelay = pc.attackPostDelay;
        attacked = false;

        pc.GetComponent<Animator>().SetTrigger("StartWeakAttack");
        pc.attackCooltime = pc.weakAttackSpeed;
    }

    override public void OnUpdate()
    {
        // Pre delay
        preDelay -= Time.deltaTime;
        if (preDelay > 0.0f)
        {
            return;
        }

        // Create attack object
        if (attacked == false)
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

            attacked = true;
        }

        // Post delay
        postDelay -= Time.deltaTime;
        if (postDelay <= 0.0f)
        {
            pc.SetState(STATE.IDLE);
            return;
        }
    }

    override public void OnEnd()
    {
    }
}