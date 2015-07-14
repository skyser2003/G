using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObject_Player : GameObjectBase {


	protected override void ProcessInput (float _deltatime)
	{
		base.ProcessInput (_deltatime);

		for(int inputiter = 0; inputiter < InputReceivedList.Count; inputiter++)
		{
			GInputType curinput = InputReceivedList[inputiter];

			if(curinput == GInputType.MOVE_LEFT_PRESSED)
			{
				MoveObject.Move(true);
			}

			if(curinput == GInputType.MOVE_RIGHT_PRESSED)
			{
				MoveObject.Move(false);
			}

			if(curinput == GInputType.JUMP_DOWN)
			{
				if(	MoveObject.Jump() )
				{
					//AnimationComp.SetTrigger("StartJump");
				}
			}

			if(curinput == GInputType.KEY_1_DOWN)
			{
				if(AttackComp.PlayAttack(0))
				{
					AnimationComp.SetTrigger("StartWeakAttack");
				}
			}
		}

		ClearInput();
	}

	protected override void ProcessAnimation (float _timer)
	{
		base.ProcessAnimation (_timer);
		AnimationComp.SetBool("IsOnAir", !MoveObject.IsOnGround);
		AnimationComp.SetFloat ("HorizontalSpeed", Mathf.Abs(MoveObject.InnerVelocity.x));
		AnimationComp.SetLeft(MoveObject.InnerVelocity.x < 0f);
	}
}
