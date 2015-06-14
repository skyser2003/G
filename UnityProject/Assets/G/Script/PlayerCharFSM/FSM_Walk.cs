using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

partial class PlayerCharacter
{
    private class FSM_Walk : AbstractFSM
    {
        override public void OnBegin()
        {
        }

        override public void OnUpdate()
        {
            pc.GetComponent<Transform>().localPosition += new Vector3(pc.speed * Time.deltaTime, 0.0f, 0.0f);
        }

        override public void OnEnd()
        {
        }
    }
}