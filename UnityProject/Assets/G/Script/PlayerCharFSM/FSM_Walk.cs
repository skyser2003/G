using UnityEngine;

class FSM_Walk : AbstractFSM
{
    override public void OnBegin()
    {
        if (pc.WalkVelocity < 0)
        {
            pc.GetComponent<Transform>().rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (pc.WalkVelocity > 0)
        {
            pc.GetComponent<Transform>().rotation = Quaternion.Euler(0, 90, 0);
        }

        if (pc.WalkVelocity != 0)
        {
            var animator = pc.GetComponent<Animator>();
            animator.SetFloat("HorizontalSpeed", pc.Walk.Speed);
        }
    }

    override public void OnUpdate()
    {
        pc.GetComponent<Transform>().localPosition += new Vector3(pc.WalkVelocity * Time.deltaTime, 0.0f, 0.0f);
    }

    override public void OnEnd()
    {
    }
}
