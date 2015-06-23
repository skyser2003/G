using UnityEngine;

class FSM_Idle : AbstractFSM
{
    override public void OnBegin()
    {
        var animator = pc.GetComponent<Animator>();
        animator.SetFloat("HorizontalSpeed", pc.Walk.Speed);
    }

    override public void OnUpdate()
    {

    }

    override public void OnEnd()
    {

    }
}