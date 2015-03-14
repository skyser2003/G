using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

partial class PlayerCharacter
{
    private class MoveFSM_Walk : AbstractFSM
    {
        private SkeletonAnimation skeleton = null;
 
        override public void OnBegin()
        {
            skeleton = pc.GetComponent<SkeletonAnimation>();

            float speed = pc.speed;
            if (speed == 0)
            {
                skeleton.AnimationName = null;
            }
            else if (speed < 0)
            {
                pc.GetComponent<Transform>().localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                skeleton.AnimationName = "Walk";
            }
            else
            {
                pc.GetComponent<Transform>().localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                skeleton.AnimationName = "Walk";
            }
        }

        override public void OnUpdate()
        {
            pc.GetComponent<Transform>().localPosition += new Vector3(pc.speed * Time.deltaTime, 0.0f, 0.0f);
        }

        override public void OnEnd()
        {
            skeleton.AnimationName = null;
        }
    }
}