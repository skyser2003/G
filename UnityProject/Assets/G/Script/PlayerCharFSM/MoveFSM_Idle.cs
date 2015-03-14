using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

partial class PlayerCharacter
{
    private class MoveFSM_Idle : AbstractFSM
    {
        override public void OnBegin()
        {
            pc.GetComponent<SkeletonAnimation>().AnimationName = null; ;
        }

        override public void OnUpdate()
        {

        }

        override public void OnEnd()
        {

        }
    }
}