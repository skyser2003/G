using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObject_Player : GameObjectBase {

	protected bool IsHoldingStringAttack = false;
	protected bool StartStrongAttack = false;
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
				if(AttackComp.CanAttack(0))
				{
					
				}

				AttackPressedTimer = 0f;
				IsHoldingStringAttack = true;
			}
			if(curinput == GInputType.KEY_1_PRESSED)
			{
				if(AttackComp.CanAttack(0))
				{
					IsHoldingStringAttack = true;
					AttackPressedTimer += Time.deltaTime;
					if(AttackPressedTimer > StrongAttackStartTime)
					{
						if(!StartStrongAttack)
						{
							StartStrongAttack = true;
							AnimationComp.SetTrigger("StartStrongAttack");
						}
					}
				}
			}

			if(curinput == GInputType.KEY_1_RELEASE)
			{
				if(AttackComp.CanAttack(0))
				{				
					if(AttackPressedTimer > StrongAttackStartTime)
					{
						if(AttackComp.PlayAttack(1))
						{
							AnimationComp.SetTrigger("ReleaseStrongAttack");
						}
						StartStrongAttack = false;
						AttackPressedTimer = 0f;
					}else
					{
						if(AttackComp.PlayAttack(0))
						{
							AnimationComp.SetTrigger("StartWeakAttack");
						}
						StartStrongAttack = false;
						AttackPressedTimer = 0f;
					}
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
		AnimationComp.SetBool("HoldStrongAttack", IsHoldingStringAttack && StartStrongAttack);
	}
}
