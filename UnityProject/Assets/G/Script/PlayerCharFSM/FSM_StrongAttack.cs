using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class FSM_StrongAttack : AbstractFSM
{
    private float preDelay;
    private float postDelay;
    bool attacked;
    public float ChargeTime = 0.0f;

    override public void OnBegin()
    {
        preDelay = pc.attackPreDelay;
        postDelay = pc.attackPostDelay;
        attacked = false;
        ChargeTime = 0.0f;

        pc.GetComponent<Animator>().SetTrigger("StartWeakAttack");
        pc.attackCooltime = pc.strongAttackSpeed;
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
            int currentCharge = (int)(ChargeTime / 1.0f);
            float damageMultiplier = 1.0f;
            if(currentCharge > pc.strongAttackBestCharge)
            {
                damageMultiplier = 0.5f;
            }

            var info = DataManager.Inst.GetAttackPattern("ID_2");

            var obj = new GameObject();
            var atkManagerObj = obj.AddComponent<AttackManagerObject>();
            atkManagerObj.Init(pc.gameObject, info, damageMultiplier);
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