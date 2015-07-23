using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObject_Lich : GameObjectBase {

	public GameObject Player;

	public GameObjectPart CurOrb;
	public GameObjectPart Orb1;
	public GameObjectPart Orb2;
	public GameObjectPart Orb3;

	public GAttack_Lich_1 AttackPattern1;
	public GAttack_Lich_2 AttackPattern2;
	public GAttack_Lich_3 AttackPattern3;

	protected bool IsLeft = false;

	public List<Transform> OrbPosList = new List<Transform>();
	public override void Create ()
	{
		base.Create ();
		InitAIState();

		CurOrb = Orb1;
		Debug.Log("?");
	}

	public override void Process (float _deltatime)
	{
		ProcessAI(_deltatime);
		UpdateOrbPos(_deltatime);
		base.Process (_deltatime);
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

	public enum LichState
	{
		IDLE = 0,
		ATTACK_1 = 1,
		ATTACK_2 = 2,
		ATTACK_3 = 3,
		EXHAUSTED = 4,

	}

	public List<GAIBase> AIBaseList = new List<GAIBase>();
	protected GAIBase CurState;

	protected void InitAIState()
	{
		CurState = AIBaseList[0];
	}

	protected bool CanChangeToState(LichState _state)
	{
		if(_state == LichState.ATTACK_1)
		{
			if(!Orb1.IsDead)
			{
				return true;
			}
			return false;
		}else if(_state == LichState.ATTACK_2)
		{
			if(!Orb2.IsDead)
			{
				return true;
			}
			return false;
		}else if(_state == LichState.ATTACK_3)
		{
			if(!Orb3.IsDead)
			{
				return true;
			}
			return false;
		}

		return true;
	}

	protected void CheckChangeState()
	{		
		if(CurState.StateIndex == (int)LichState.IDLE)
		{
			ChangeAIState(LichState.ATTACK_1);
		}else if(CurState.StateIndex == (int)LichState.ATTACK_1)
		{
			if(CurState.PlayingTimer > 10f)
			{
				ChangeAIState(LichState.ATTACK_2);
			}
		}else if(CurState.StateIndex == (int)LichState.ATTACK_2)
		{
			if(CurState.PlayingTimer > 10f)
			{
				ChangeAIState(LichState.ATTACK_3);
			}
		}else if(CurState.StateIndex == (int)LichState.ATTACK_3)
		{
			if(CurState.PlayingTimer > 10f)
			{
				ChangeAIState(LichState.ATTACK_1);
			}
		}
	}

	protected void ChangeAIState(LichState _state)
	{
		CurState.Reset();
		for(int iter = 0; iter < AIBaseList.Count; iter++)
		{
			if((LichState)AIBaseList[iter].StateIndex == _state)
			{
				CurState = AIBaseList[iter];
			}
		}

		if(CurState.StateIndex == (int)LichState.ATTACK_1)
		{
			CurOrb = Orb1;
		}else if(CurState.StateIndex == (int)LichState.ATTACK_2)
		{
			CurOrb = Orb2;
		}else if(CurState.StateIndex == (int)LichState.ATTACK_3)
		{
			CurOrb = Orb3;
		}
	}


	protected void ProcessAI(float _deltatime)
	{
		if(IsDead)
		{
			return;
		}

		CheckChangeState();

		CurState.Process(_deltatime);
		if(CurState.StateIndex == (int)LichState.ATTACK_1)
		{
			if(!Orb1.IsDead && AttackPattern1.CanAttack())
			{
				AttackPattern1.SetTarget(Player.transform);
				AttackPattern1.Play();
			}
		}else if(CurState.StateIndex == (int)LichState.ATTACK_2)
		{
		
			if(!Orb2.IsDead && AttackPattern2.CanAttack())
			{
				AttackPattern2.Play();
			}
		}else if(CurState.StateIndex == (int)LichState.ATTACK_3)
		{		
			if(!Orb3.IsDead && AttackPattern3.CanAttack())
			{
				AttackPattern3.Play();
			}
		}
	}

	protected void UpdateOrbPos(float _deltatime)
	{
		List<Transform> orborderlist = new List<Transform>();
		orborderlist.Add(CurOrb.transform);
		if(CurOrb == Orb1)
		{
			orborderlist.Add(Orb2.transform);
			orborderlist.Add(Orb3.transform);
		}
		if(CurOrb == Orb2)
		{
			orborderlist.Add(Orb1.transform);
			orborderlist.Add(Orb3.transform);
		}
		if(CurOrb == Orb3)
		{
			orborderlist.Add(Orb1.transform);
			orborderlist.Add(Orb2.transform);
		}

		for(int transformiter = 0; transformiter < orborderlist.Count; transformiter++)
		{
			orborderlist[transformiter].transform.position = Vector3.Lerp(orborderlist[transformiter].position, 
			                                                              OrbPosList[transformiter].transform.position, _deltatime);
		}

	}
}
