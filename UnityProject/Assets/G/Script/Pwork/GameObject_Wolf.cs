using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObject_Wolf : GameObjectBase {

	protected bool IsLeft = false;
	public override void Create ()
	{
		base.Create ();

	}
		
	public override void Process (float _deltatime)
	{
		base.Process (_deltatime);
		ProcessAI (_deltatime);
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
	public enum WolfState
	{
		IDLE 		= 0,
		SEARCH 		= 1,
		ATTACK 		= 2,
	}

	public List<GAIBase> AIBaseList = new List<GAIBase>();
	protected GAIBase CurState;
	//public List<GAI<WolfState>> AIList = new List<GAI<WolfState>>();

	public bool Searching = false;
	protected WolfState CurAIstate = WolfState.IDLE;
	protected void InitAIState()
	{
		Searching = true;
		CurState = AIBaseList[0];
	}

	protected void ProcessAI(float _deltatime)
	{
		CurState.Process(_deltatime);

		CheckChangeState();
		if(CurAIstate == WolfState.IDLE)
		{

		}
	}

	protected void CheckChangeState()
	{
		if(CurState.StateIndex == (int)WolfState.IDLE)
		{
			//check time
			if(CurState.PlayingTimer > 1f)
			{
				ChangeAIState(WolfState.SEARCH);
			}
		}else if(CurState.StateIndex == (int)WolfState.SEARCH)
		{
			if(CurState.PlayingTimer > 2f)
			{
				ChangeAIState(WolfState.IDLE);
			}
		}
	}

	protected void ChangeAIState(WolfState _state)
	{
		CurState.Reset();
		for(int iter = 0; iter < AIBaseList.Count; iter++)
		{
			if((WolfState)AIBaseList[iter] == _state)
			{
				CurState = AIBaseList[iter];
			}
		}
	}

	protected void SearchForPlayer()
	{

	}
	//public List<GAI> AIList = new List<GAI>();

}
