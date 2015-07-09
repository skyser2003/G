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
    float time;

    override public void OnBegin()
    {
        preDelay = pc.attackPreDelay;
        postDelay = pc.attackPostDelay;
        attacked = false;
        time = 0.0f;

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
            var info = DataManager.Inst.GetAttackPattern("ID_1");

            var obj = new GameObject();
            var atkManagerObj = obj.AddComponent<AttackManagerObject>();
            atkManagerObj.Init(pc.gameObject, info, 1.0f);

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