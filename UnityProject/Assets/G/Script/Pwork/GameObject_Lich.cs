using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObject_Lich : GameObjectBase {

	public GameObject Player;

	public GameObjectPart Orb1;
	public GameObjectPart Orb2;
	public GameObjectPart Orb3;

	public GAttack_Lich_1 AttackPattern1;
	public GAttack_Lich_2 AttackPattern2;

	protected bool IsLeft = false;
	public override void Process (float _deltatime)
	{
		base.Process (_deltatime);
		if(AttackPattern1.CanAttack())
		{
			AttackPattern1.SetTarget(Player.transform);
			AttackPattern1.Play();
		}

		if(AttackPattern2.CanAttack())
		{
			AttackPattern2.Play();
		}
	}

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
				
			}
		}

		ClearInput();
	}

	protected override void ProcessAnimation (float _timer)
	{
		base.ProcessAnimation (_timer);


		AnimationComp.SetFloat ("HorizontalSpeed", Mathf.Abs(MoveObject.InnerVelocity.x));
		//AnimationComp.SetLeft(IsLeft);
		AnimationComp.SetBool("IsDead", IsDead);

	}

	public override void Hit (float _damage)
	{
		base.Hit (_damage);
		if(!IsDead)
		{
			AnimationComp.SetTrigger("StartDamaged");
		}
	}
}
