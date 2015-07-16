using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObject_Player : GameObjectBase {

	protected bool IsHoldingStringAttack = false;
	public float StrongAttackStartTime = 0.05f;
	protected float AttackPressedTimer;
	protected bool IsLeft = false;
	protected override void ProcessInput (float _deltatime)
	{
		base.ProcessInput (_deltatime);
		IsHoldingStringAttack = false;
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
					IsHoldingStringAttack = true;
				}

				AttackPressedTimer = 0f;
			}
			if(curinput == GInputType.KEY_1_PRESSED)
			{
				if(AttackCompList[0].CanAttack())
				{
					IsHoldingStringAttack = true;
					AttackPressedTimer += Time.deltaTime;
					if(AttackPressedTimer > StrongAttackStartTime)
					{

					}
				}
			}

			if(curinput == GInputType.KEY_1_RELEASE)
			{
				if(AttackCompList[0].CanAttack())
				{
					if(AttackCompList[0].PlayAttack())
					{
						AnimationComp.SetTrigger("ReleaseAttack");
					}
					AttackPressedTimer = 0f;
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
		AnimationComp.SetLeft(IsLeft);
		AnimationComp.SetBool("HoldAttack", IsHoldingStringAttack);
	}
}
