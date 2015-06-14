using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

partial class PlayerCharacter
{
    private class FSM_BaseAttack : AbstractFSM
    {
        private SkeletonAnimation skeleton = null;
        private float attackTime = 1.0f;

        override public void OnBegin()
        {
            skeleton = pc.GetComponent<SkeletonAnimation>();
            skeleton.AnimationName = "Attack";
            skeleton.loop = true;
        }

        override public void OnUpdate()
        {
            attackTime -= Time.deltaTime;
            if(attackTime <= 0.0f)
            {
                pc.SetState(STATE.IDLE);
                return;
            }

            if(pc.speed >= 0.0f)
            {
                pc.speed -= Time.deltaTime;
                if(pc.speed < 0.0f)
                {
                    pc.speed = 0.0f;
                }
            }
        }

        override public void OnEnd()
        {
            skeleton.AnimationName = null;
        }
    }
}