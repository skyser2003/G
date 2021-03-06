using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObject_Spearman : GameObjectBase {

	public Transform TargetPlayer;

	protected bool IsLeft = false;

	public GAttackPatternBase BiteAttack;

	public bool HasGroundForward = true;
	public Transform AIGroundChecker;

	public Transform PlayerSearchRoot;
	public float PlayerFindDistance = 4f;

	void OnDrawGizmos()
	{
		//Vector2 direction = Vector2.right;
		//if(IsLeft)
		//{
		//	direction = Vector2.left;
		//}
		//Gizmos.color = Color.red;
		//
		//Gizmos.DrawRay(transform.position, direction * PlayerFindDistance);
	}

	public override void Create ()
	{
		base.Create ();
		InitAIState();
	}
		
	public override void Process (float _deltatime)
	{
		if(!IsDead)
		{
			ProcessAI (_deltatime);
		}
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
				DirectionPos = new Vector3(0f, 220f, 0f);
				MoveObject.Move(true);
			}

			if(curinput == GInputType.MOVE_RIGHT_PRESSED)
			{
				IsLeft = false;
				DirectionPos = new Vector3(0f, 140f, 0f);
				MoveObject.Move(false);
			}

			if(curinput == GInputType.JUMP_DOWN)
			{
				if(	MoveObject.Jump() )
				{
					//AnimationComp.SetTrigger("StartJump");
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
		Follow 		= 3,
	}

	public List<GAIBase> AIBaseList = new List<GAIBase>();
	protected GAIBase CurState;
	//public List<GAI<WolfState>> AIList = new List<GAI<WolfState>>();

	public bool Searching = false;
	protected void InitAIState()
	{
		Searching = false;
		CurState = AIBaseList[0];
	}

	protected void ProcessAI(float _deltatime)
	{
		CheckGroundForward();
		CurState.Process(_deltatime);

		CheckChangeState();
		if((WolfState)CurState.StateIndex == WolfState.IDLE)
		{

		}else if((WolfState)CurState.StateIndex == WolfState.SEARCH)
		{
			//make input
			if(IsLeft)
			{
				if(!HasGroundForward)
				{
					IsLeft = !IsLeft;
				}else
				{
					AddInput(GInputType.MOVE_LEFT_PRESSED);				
				}
			}else
			{
				if(!HasGroundForward)
				{
					IsLeft = !IsLeft;
				}else
				{
					AddInput(GInputType.MOVE_RIGHT_PRESSED);
				}
			}
		}else if((WolfState)CurState.StateIndex == WolfState.Follow)
		{
			//make input
			if(TargetPlayer != null)
			{
				float deltax = TargetPlayer.transform.position.x - transform.position.x;
				IsLeft = deltax < 0f ? true : false;
				if(IsLeft)
				{
					if(HasGroundForward)
					{
						AddInput(GInputType.MOVE_LEFT_PRESSED);
					}
				}else
				{
					if(HasGroundForward)
					{
						AddInput(GInputType.MOVE_RIGHT_PRESSED);
					}
				}
			}
		}
	}

	protected void CheckGroundForward()
	{
		HasGroundForward = false;

		RaycastHit2D[] hitlist = Physics2D.RaycastAll(AIGroundChecker.position, Vector2.down, 1f, LayerMask.GetMask(Constant.Layer_Ground));
		if(hitlist.Length > 0)
		{
			HasGroundForward = true;
		}
	}

	protected void CheckChangeState()
	{
		CheckForPlayer();

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
		}else if(CurState.StateIndex == (int)WolfState.Follow)
		{
			if(TargetPlayer == null)
			{
				ChangeAIState(WolfState.SEARCH);
			}else if(Mathf.Abs(TargetPlayer.position.x - transform.position.x) > 4f)
			{
				TargetPlayer = null;
				ChangeAIState(WolfState.SEARCH);
			}else if(Mathf.Abs(TargetPlayer.position.x - transform.position.x) < 2f)
			{
				ChangeAIState(WolfState.ATTACK);
			}
		}else if(CurState.StateIndex == (int)WolfState.ATTACK)
		{
			if(BiteAttack.IsPlaying)
			{
				//dont do anything
			}else if(!BiteAttack.IsPlaying)
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
			if((WolfState)AIBaseList[iter].StateIndex == _state)
			{
				CurState = AIBaseList[iter];
			}
		}

		if((WolfState)CurState.StateIndex == WolfState.SEARCH)
		{
			IsLeft = Random.Range(0f,100f) < 50f ? true : false;
		}

		if((WolfState)CurState.StateIndex == WolfState.ATTACK)
		{
			if(BiteAttack.CanAttack())
			{
				BiteAttack.Play();
				AnimationComp.SetTrigger("StartAttack");
			}else
			{
				ChangeAIState(WolfState.IDLE);
			}
		}
	}

	protected void CheckForPlayer()
	{
		if((WolfState)CurState.StateIndex != WolfState.Follow
		   && (WolfState)CurState.StateIndex != WolfState.ATTACK)
		{
			Vector2 direction = Vector2.right;
			if(IsLeft)
			{
				direction = Vector2.left;
			}
			RaycastHit2D[] hitinfo = Physics2D.RaycastAll(PlayerSearchRoot.position, direction, PlayerFindDistance, LayerMask.GetMask(Constant.Layer_Player));
			if(hitinfo.Length > 0)
			{
				//Debug.Log("found player");
				TargetPlayer = hitinfo[0].transform;
				ChangeAIState(WolfState.Follow);
			}else
			{
				TargetPlayer = null;
			}
		}
	}
	//public List<GAI> AIList = new List<GAI>();

}
