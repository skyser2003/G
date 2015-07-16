using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObject_Wolf : GameObjectBase {

	protected bool IsLeft = false;
	protected override void ProcessInput (float _deltatime)
	{
		base.ProcessInput (_deltatime);
		//Debug.Log("?: " + InputReceivedList.Count);
		for(int inputiter = 0; inputiter < InputReceivedList.Count; inputiter++)
		{
			GInputType curinput = InputReceivedList[inputiter];
			//Debug.Log("wat: " + curinput);
			if(curinput == GInputType.MOVE_LEFT_PRESSED)
			{
				IsLeft = true;
				DirectionPos = Vector3.left;
				MoveObject.Move(true);
			}

			if(curinput == GInputType.MOVE_RIGHT_PRESSED)
			{
				IsLeft = false;
				DirectionPos = Vector3.right;
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
				//if(AttackComp.PlayAttack(0))
				//{
				//	AnimationComp.SetTrigger("StartWeakAttack");
				//}
				if(AttackCompList[0].CanAttack())
				{
				}

			}
			if(curinput == GInputType.KEY_1_PRESSED)
			{

			}

			if(curinput == GInputType.KEY_1_RELEASE)
			{
				if(AttackCompList[0].CanAttack())
				{
					if(AttackCompList[0].PlayAttack())
					{
						AnimationComp.SetTrigger("ReleaseAttack");
					}
				}
			}
		}

		ClearInput();
	}

	protected override void ProcessAnimation (float _timer)
	{
		base.ProcessAnimation (_timer);
		AnimationComp.SetFloat ("HorizontalSpeed", Mathf.Abs(MoveObject.InnerVelocity.x));
		AnimationComp.SetLeft(IsLeft);
	}

	public override void Hit (float _damage)
	{
		base.Hit (_damage);
		AnimationComp.SetTrigger("StartDamaged");
	}
}
